using Architecture.DataService.Data;
using Architecture.DataService.IConfiguration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:secret"]);
var tokenValidationParameter = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    ValidateAudience = false,
    ValidateIssuer = false,
    ValidateLifetime = true,
    IssuerSigningKey = new SymmetricSecurityKey(key) 
};

builder.Services.AddAuthentication(options=>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = tokenValidationParameter;
});

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();