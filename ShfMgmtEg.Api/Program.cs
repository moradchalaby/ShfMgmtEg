using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShfMgmtEg.Core;
using ShfMgmtEg.Core.Dtos.User;
using ShfMgmtEg.Data;
using ShfMgmtEg.Service;
using ShfMgmtEg.Service.AuthService;
using ShfMgmtEg.Service.EmployeeService;
using ShfMgmtEg.Service.ShiftService;
using ShfMgmtEg.Service.TeamService;
using ShfMgmtEg.Service.UserService;
using ShfMgmtEg.Service.Validation.Auth;
using ShfMgmtEg.Services.TeamService;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DataContext>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new() { Title = "ShfMgmtEg", Version = "v1" });
    x.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });
    x.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value ?? throw new InvalidOperationException())),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
//Service Katmanının kullanılabilmesi için buraya ekliyoruz.
//Scoped ile eklersek her seferinde yeni bir instance oluşturur.
//Singleton ile eklersek her seferinde aynı instance kullanılır.
//Transient ile eklersek her seferinde yeni bir instance oluşturur.
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IValidator<RegisterUser>,RegisterValidation>();
builder.Services.AddScoped<IValidator<LoginUser>,LoginValidation>();
builder.Services.AddScoped<IShiftService, ShiftService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITeamService, TeamService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    SeedService.Initialize(app.Services,app.Configuration);
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();




app.Run();
