using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using SquareUp.Server.Data;
using SquareUp.Server.Models;
using SquareUp.Shared.Types;

namespace SquareUp.Server.Services.Auth;

public class AuthService : IAuthService
{
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<ServiceResponse<User>> Register(UserData user, string password)
    {
        if (await UserExists(user.Email))
        {
            return new ServiceResponse<User>(message: $"{user.Email} is already in use.");
        }

        CreatePasswordHash(password, out var hash, out var salt);
        user.PasswordHash = hash;
        user.PasswordSalt = salt;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new ServiceResponse<User>(user, $"{user.Email} successfully registered.");
    }

    public async Task<bool> UserExists(string email)
    {
        email = email.ToLower();

        return await _context.Users.AnyAsync(user => user.Email.ToLower() == email);
    }

    public async Task<ServiceResponse<string>> Login(string email, string password)
    {
        email = email.ToLower();

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email);

        if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            return new ServiceResponse<string>(message: "Incorrect username or password.");
        }

        return new ServiceResponse<string>(CreateToken(user));
    }

    public async Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return new ServiceResponse<bool>(message: "User not found.");

        CreatePasswordHash(newPassword, out var hash, out var salt);
        user.PasswordHash = hash;
        user.PasswordSalt = salt;

        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>(true, "Password changed successfully.");
    }

    public int GetUserId()
    {
        return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }

    public string GetUserEmail()
    {
        return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
    }

    public async Task<UserData> GetUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
    }

    public bool IsAdmin()
    {
        throw new NotImplementedException();
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }

    private string CreateToken(UserData user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Email, user.Email),
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}