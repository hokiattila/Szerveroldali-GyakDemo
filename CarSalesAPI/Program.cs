using Microsoft.EntityFrameworkCore;
using CarSalesAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Connection string lek�r�se �s DbContext regisztr�l�sa
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CarSalesContext>(options =>
    options.UseSqlServer(connectionString));

// Controllers hozz�ad�sa
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
