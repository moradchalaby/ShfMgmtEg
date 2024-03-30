using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore;
using ShfMgmtEg.Core.Entities;
using ShfMgmtEg.Core.Entities.Models;

namespace ShfMgmtEg.Data;

public class DataContext : DbContext
{
    public DataContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        JsonDocument doc = JsonDocument.Parse(System.IO.File.ReadAllText("../ShfMgmtEg.Api/appsettings.json"));
        JsonElement root = doc.RootElement;
        string? connectionString = root.GetProperty("ConnectionStrings").GetProperty("DefaultConnection").GetString();
        Console.WriteLine(connectionString);
        optionsBuilder.UseSqlServer(connectionString);


    }
    public DbSet<User> Users { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    
    private void SetTimestamps()
    {
        var entries = ChangeTracker.Entries();
        var currentTime = DateTime.UtcNow; // Geçerli tarih ve saat

        foreach (var entry in entries)
        {
            if (entry.Entity is BaseEntity entity)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedAt = currentTime;
                        entity.UpdatedAt = currentTime;
                        entity.IsDeleted = false;
                        entity.DeletedAt = null;
                        break;
                    case EntityState.Modified:
                       entity.UpdatedAt = currentTime; ;
                       break;
                    case EntityState.Deleted:
                        entity.DeletedAt = currentTime;
                        entity.IsDeleted = true;
                        break;
                }
            }
        }
    }
}
   
    