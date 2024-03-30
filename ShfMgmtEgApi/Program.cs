using ShfMgmtEgApi.Services.EmployeeService;

var builder = WebApplication.CreateBuilder(args);

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
