using System.Text.Json;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShfMgmtEg.Core.Entities;
using ShfMgmtEg.Core.Entities.Models;
using ShfMgmtEg.Core.Entities.Models.Relationships;

namespace ShfMgmtEg.Data;

public class DataContext : DbContext
{
    public DataContext()
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<TeamEmployee> TeamEmployees { get; set; }
    public DbSet<RoleUser> RoleUser { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    public DbSet<ShiftTeam> ShiftTeams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var doc = JsonDocument.Parse(File.ReadAllText("../ShfMgmtEg.Api/appsettings.json"));
        var root = doc.RootElement;
        var connectionString = root.GetProperty("ConnectionStrings").GetProperty("DefaultConnection").GetString();
        Console.WriteLine(connectionString);
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeamEmployee>()
            .HasKey(te => new { te.TeamId, te.EmployeeId });
        modelBuilder.Entity<TeamEmployee>()
            .HasOne(te => te.Team)
            .WithMany(t => t.TeamEmployees)
            .HasForeignKey(te => te.TeamId);
        modelBuilder.Entity<TeamEmployee>()
            .HasOne(te => te.Employee);
        modelBuilder.Entity<RoleUser>()
            .HasKey(ru => new { ru.RoleId, ru.UserId });
        modelBuilder.Entity<RoleUser>()
            .HasOne(ru => ru.Role)
            .WithMany(r => r.RoleUsers)
            .HasForeignKey(ru => ru.RoleId);
        modelBuilder.Entity<RoleUser>()
            .HasOne(ru => ru.User)
            .WithMany(u => u.RoleUser)
            .HasForeignKey(ru => ru.UserId);
        modelBuilder.Entity<ShiftTeam>()
            .HasKey(st => new { st.ShiftId, st.TeamId });
        modelBuilder.Entity<ShiftTeam>()
            .HasOne(st => st.Shift)
            .WithMany(st => st.ShiftTeams);
        modelBuilder.Entity<ShiftTeam>()
            .HasOne(st => st.Team)
            .WithMany(st => st.ShiftTeams);
    }

    private void SetTimestamps()
    {
        var entries = ChangeTracker.Entries();
        var currentTime = DateTime.UtcNow; // Geçerli tarih ve saat

        foreach (var entry in entries)
            if (entry.Entity is BaseEntity entity)
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.Id = entry.Context.Set<BaseEntity>().Max(x => x.Id) + 1;
                        entity.CreatedAt = currentTime;
                        entity.UpdatedAt = currentTime;
                        entity.IsDeleted = false;
                        entity.DeletedAt = null;
                        break;
                    case EntityState.Modified:
                        entity.UpdatedAt = currentTime;
                        ;
                        break;
                    case EntityState.Deleted:
                        entity.DeletedAt = currentTime;
                        entity.IsDeleted = true;
                        break;
                }
    }
    
}