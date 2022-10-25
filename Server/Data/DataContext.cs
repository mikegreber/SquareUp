using Microsoft.EntityFrameworkCore;
using SquareUp.Server.Models;
using SquareUp.Shared.Models;

// using Shared;

namespace SquareUp.Server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    public DbSet<UserData> Users { get; set; }
    public DbSet<GroupData> Groups { get; set; }
    public DbSet<TransactionData> Transactions { get; set; }
    public DbSet<Participant> Participants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserData>().HasMany(u => u.Groups).WithMany(g => g.Users);
        modelBuilder.Entity<GroupData>().HasMany(g => g.Users).WithMany(u => u.Groups);
        modelBuilder.Entity<GroupData>().HasMany(g => g.Participants);
        modelBuilder.Entity<TransactionData>().HasOne(e => e.Group).WithMany(g => g.Transactions);
    }
}