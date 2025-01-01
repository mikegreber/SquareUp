using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using SquareUp.Server.Data;
using SquareUp.Server.Services.Auth;
using SquareUp.Server.Services.Transactions;
using SquareUp.Server.Services.Groups;
using SquareUp.Server.Services.Users;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite("Data Source=app.db");
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
    // .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
    // .EnableTokenAcquisitionToCallDownstreamApi()
    // .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))
    // .AddInMemoryTokenCaches()
    // .AddDownstreamWebApi("DownstreamApi", builder.Configuration.GetSection("DownstreamApi"))
    // .AddInMemoryTokenCaches();
builder.Services.AddAuthorization();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

#if RELEASE
app.Services.GetRequiredService<DataContext>().Database.Migrate();
#endif

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

#if RELEASE
app.UseHttpsRedirection();
#endif

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var scope = app.Services.CreateScope();
scope.ServiceProvider.GetRequiredService<DataContext>().Database.EnsureCreated();
scope.Dispose();

app.Run();
