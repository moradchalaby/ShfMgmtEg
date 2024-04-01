using Bogus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShfMgmtEg.Core.Entities.Models;
using ShfMgmtEg.Core.Entities.Models.Relationships;
using ShfMgmtEg.Core.Enums;
using ShfMgmtEg.Data;

namespace ShfMgmtEg.Service;

public class SeedService
{
 
    public static void Initialize(IServiceProvider serviceProvider,IConfiguration configuration)
    {
        using (var context = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider
                   .GetService<DataContext>())
        {
            if (!context.Users.Any()) InitializeA(context, configuration);

            if (context.Users.Any()) Initialize1(context);
            
            if (!context.Roles.Any()) Initialize2(context);
            
            if (!context.RoleUser.Any()) Initialize3(context);
            
            if (!context.Teams.Any()) Initialize4(context);
            
            if (!context.Employees.Any()) Initialize5(context);
            
            if (!context.TeamEmployees.Any()) Initialize6(context);
            
            if (!context.Shifts.Any()) Initialize7(context);
            
            if (!context.ShiftTeams.Any()) Initialize8(context);
            
            
        }
    }

    private static void InitializeA(DataContext context,IConfiguration configuration)
    {
    var authservice = new AuthService.AuthService(context, configuration);
    authservice.CreatePasswordHash("123456", out var passwordHash, out var passwordSalt);
        var user = new User
        {
            FirstName = "Admin",
            LastName = "Admin",
            Email = "admin@admin.com",
            PhoneNumber = "1234567890",
            UserName = "admin",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            IsDeleted = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        context.Users.AddRange(user);
        context.SaveChanges();
    }

    private static void Initialize1(DataContext context)
    {
        var data = new Faker<User>()
            .RuleFor(x => x.FirstName, f => f.Name.FullName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(x => x.UserName, f => f.Internet.UserName())
            .RuleFor(x => x.PasswordHash, f => f.Random.Bytes(10))
            .RuleFor(x => x.PasswordSalt, f => f.Random.Bytes(10))
            .RuleFor(x => x.IsDeleted, f => false)
            .RuleFor(x => x.CreatedAt, f => f.Date.Past())
            .RuleFor(x => x.UpdatedAt, f => f.Date.Past())
            .Generate(25);
      context.Users.AddRange(data);

        context.SaveChanges();
    }

    private static void Initialize2(DataContext context)
    {
        context.Roles.AddRange(
            new Role
            {
                Name = "Admin",
                Description = "Admin role"
            },
            new Role
            {
                Name = "User",
                Description = "User role"
            }
        );

        context.SaveChanges();
    }

    private static void Initialize3(DataContext context)
    {
        var roleUsers = new Faker<RoleUser>()
            .RuleFor(x => x.RoleId, f => f.Random.Int(1,2))
            .RuleFor(x => x.UserId, f => f.IndexVariable++ +2)
            .Generate(25);
        context.AddRange(
            new RoleUser
            {
                RoleId = 1,
                UserId = 1
            }
            );
        context.RoleUser.AddRange(
            roleUsers
        );
        context.SaveChanges();
    }

    private static void Initialize4(DataContext context)
    {
        var teams = new Faker<Team>()
            .RuleFor(x => x.Name, f => f.Name.JobArea())
            .RuleFor(x => x.Description, f => f.Lorem.Sentence())
            .RuleFor(x => x.CreatedAt, f => f.Date.Past())
            .RuleFor(x => x.UpdatedAt, f => f.Date.Past())
            .RuleFor(x => x.IsDeleted, f => false)
            .RuleFor(x => x.ManagerId, f => f.Random.Int(1, 25))
            .Generate(5);
        context.Teams.AddRange(
            teams);
        context.SaveChanges();
    }

    private static void Initialize5(DataContext context)
    {
        var employees = new Faker<Employee>()
            .RuleFor(x => x.UserId, f => f.IndexVariable++  + 1)
            .RuleFor(x => x.TeamId, f => f.Random.Int(1, 5))
            .RuleFor(x => x.Code, f => f.Random.String2(5))
            .Generate(25);
        context.Employees.AddRange(
            employees
        );
        context.SaveChanges();
    }

    private static void Initialize6(DataContext context)
    {
        var teamEmployees = new Faker<TeamEmployee>()
            .RuleFor(x => x.TeamId, f => f.Random.Int(1,5))
            .RuleFor(x => x.EmployeeId, f => f.IndexVariable++  + 1)
            .Generate(25);
        context.TeamEmployees.AddRange(
            teamEmployees
        );
        context.SaveChanges();
    }

    private static void Initialize7(DataContext context)
    {
        var shifts = new Faker<Shift>()
            .RuleFor(x => x.ManagerId, f => f.Random.Int(1, 5))
            .RuleFor(x => x.StartTime, f => f.Date.Past())
            .RuleFor(x => x.EndTime, f => f.Date.Future())
            .RuleFor(x => x.Description, f => f.Lorem.Sentence())
            .RuleFor(x => x.Name, f => f.Name.JobTitle())
            .RuleFor(x => x.PeriodicType, f => f.PickRandom<Periot>())
            .Generate(25);
        context.Shifts.AddRange(
            shifts
        );
        context.SaveChanges();
    }

    private static void Initialize8(DataContext context)
    {
        var shiftTeams = new Faker<ShiftTeam>()
            .RuleFor(x => x.ShiftId, f => f.IndexVariable++ + 1)
            .RuleFor(x => x.TeamId, f => f.Random.Int(1,5))
            .Generate(25);

        context.ShiftTeams.AddRange(
            shiftTeams
        );

        context.SaveChanges();
    }
}