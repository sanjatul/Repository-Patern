using Microsoft.EntityFrameworkCore;
using RepositoryPatern.Data.Access;
using RepositoryPatern.Services.IRepositories;
using RepositoryPatern.Services.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection")
    , ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")),
    options => options.EnableRetryOnFailure(
maxRetryCount: 5,
maxRetryDelay: System.TimeSpan.FromSeconds(30),
errorNumbersToAdd: null)
));
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
