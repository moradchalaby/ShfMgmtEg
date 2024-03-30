using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShfMgmtEg.Core.Dtos.User;
using ShfMgmtEg.Core.Entities.Models;
using ShfMgmtEg.Data;
using ShfMgmtEg.Service.AuthService;
using ShfMgmtEg.Service.EmployeeService;
using ShfMgmtEg.Service.UserService;
using ShfMgmtEg.Service.Validation.Auth;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DataContext>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//Service Katmanının kullanılabilmesi için buraya ekliyoruz.
//Scoped ile eklersek her seferinde yeni bir instance oluşturur.
//Singleton ile eklersek her seferinde aynı instance kullanılır.
//Transient ile eklersek her seferinde yeni bir instance oluşturur.
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IValidator<RegisterUser>,RegisterValidation>();
builder.Services.AddScoped<IValidator<LoginUser>,LoginValidation>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();




app.Run();
