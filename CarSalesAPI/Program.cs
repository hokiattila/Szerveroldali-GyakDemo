using Microsoft.EntityFrameworkCore;
using CarSalesAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Connection string lekérése és DbContext regisztrálása
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CarSalesContext>(options =>
    options.UseSqlServer(connectionString));

// Controllers hozzáadása
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
