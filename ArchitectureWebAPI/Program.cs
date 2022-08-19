using Architecture.DataService.Data;
using Architecture.DataService.IConfiguration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();