using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryPatern.Data.Access;
using RepositoryPatern.Services.IRepositories;
using RepositoryPatern.Services.Repositories;

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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
