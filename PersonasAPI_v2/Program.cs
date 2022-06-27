using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PersonasAPI_v2.Configuration;
using PersonasAPI_v2.Repository;
using PersonasAPI_v2.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var connString = builder.Configuration.GetConnectionString("SQLServerConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connString);
});

builder.Services.AddTransient<IProductosRepository, ProductosRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();

//JWT
var jwtSecreto = builder.Configuration["AppSettings:JwtSecreto"];
var key = Encoding.ASCII.GetBytes(jwtSecreto);
builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(d => {

    d.RequireHttpsMetadata = false;
    d.SaveToken = true;
    d.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();