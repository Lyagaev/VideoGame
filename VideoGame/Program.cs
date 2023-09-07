using Microsoft.EntityFrameworkCore;
using VideoGame.Abstractions;
using VideoGame.Entities;
using VideoGame.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(db =>
{
    db.UseSqlServer(connection);
    db.UseSnakeCaseNamingConvention();
});

builder.Services.AddScoped(typeof(DbContext), typeof(DataContext));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

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


