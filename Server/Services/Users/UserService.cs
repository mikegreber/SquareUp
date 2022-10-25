using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SquareUp.Server.Data;
using SquareUp.Server.Models;
using SquareUp.Shared.Models;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;

namespace SquareUp.Server.Services.Users;

public class UserService : IUserService
{
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;

    public UserService(DataContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public Task<ServiceResponse<User>> GetCurrentUser()
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<List<User>>> GetUsers()
    {
        var result = await _context.Users
            .Include(u => u.Groups)
            .Select(u => new User(u))
            .ToListAsync();
        
        return new ServiceResponse<List<User>>(result, $"{result.Count} users.");
    }

    public async Task<ServiceResponse<User>> GetUser(int id)
    {
        var user = await _context.Users
            .Include(u => u.Groups)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return new ServiceResponse<User>(message: "User not found.");
        }

        return new ServiceResponse<User>(user, "User found.");
    }

    public async Task<ServiceResponse<User>> GetUser(string email)
    {
        email = email.ToLower();
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            return new ServiceResponse<User>(message: "User not found.");
        }

        return new ServiceResponse<User>(user, message: "User found");
    }

    public async Task<ServiceResponse<UserBase>> Login(LoginRequest loginRequest)
    {
        loginRequest.Email = loginRequest.Email.ToLower();
        var user = await _context.Users
            .Where(u => u.Email == loginRequest.Email)
            .FirstOrDefaultAsync();

        if (user == null || !VerifyPasswordHash(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
        {
            return new ServiceResponse<UserBase>(message: "Incorrect username or password.");
        }

        return new ServiceResponse<UserBase>(user, CreateToken(user));
    }

    public async Task<ServiceResponse<User>> Register(RegisterRequest request)
    {
        if (await UserExists(request.Email))
        {
            return new ServiceResponse<User>(message: $"{request.Email} is already in use.");
        }

        var user = new UserData{ Name = request.Name, Email = request.Email, Image = "dotnet_bot.svg" };

        CreatePasswordHash(request.Password, out var hash, out var salt);
        user.PasswordHash = hash;
        user.PasswordSalt = salt;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new ServiceResponse<User>(user, CreateToken(user));
    }

    public async Task<bool> UserExists(string email)
    {
        email = email.ToLower();

        return await _context.Users.AnyAsync(user => user.Email.ToLower() == email);
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

    private string CreateToken(User user)
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