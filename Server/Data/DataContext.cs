using Microsoft.EntityFrameworkCore;
using SquareUp.Server.Models;

// using Shared;

namespace SquareUp.Server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) => ChangeTracker.LazyLoadingEnabled = false;

    // public DbSet<Expense> Expenses { get; set; }
    public DbSet<UserData> Users { get; set; }
    public DbSet<GroupData> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.Entity<User>().HasMany(u => u.Groups).WithMany(g => g.Users);
        // modelBuilder.Entity<Group>().HasMany(g => g.Users).WithMany(u => u.Groups);
        // //
        // modelBuilder.Entity<User.UserData>().HasData(
        //     new User.UserData { Id = 1, Name = "Mike" },
        //     new User.UserData { Id = 2, Name = "Lorii" }
        // );

        // modelBuilder.Entity<Expense>().HasKey(e => new { e.Id, e.UserId });
    }
}