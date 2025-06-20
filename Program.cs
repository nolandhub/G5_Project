using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using webAPI.Data;
using MyWebApi.Helpers;
using MyWebApi.Interfaces.IServices;
using MyWebApi.Services;
using MyWebApi.Middleware;
using Microsoft.Extensions.Options;
using MyWebApi.Interfaces.IRepositories;
using MyWebApi.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;

var builder = WebApplication.CreateBuilder(args);

// ========================
// REGISTER CORE SERVICES
// ========================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpContextAccessor();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// ========================
// SWAGGER CONFIGURATION
// ========================
builder.Services.AddSwaggerGen(Options =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = " Enter Your JWT Access Token",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    Options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
    Options.AddSecurityRequirement(new OpenApiSecurityRequirement{
            { jwtSecurityScheme,Array.Empty<string>()}
    });
});

// ========================
// DATABASE CONFIGURATION
// ========================
builder.Services.AddDbContext<AppDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ========================
// DEPENDENCY INJECTION
// ========================
// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JwtAuthToken>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Repositories
builder.Services.AddScoped<ITaiKhoanRepo, TaiKhoanRepo>();

// ========================
// AUTHENTICATION & AUTHORIZATION
// ========================
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Key"]!)),
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});

// Authorization Policies
builder.Services.AddAuthorization(options =>
{
    // Policy for Admin role
    options.AddPolicy("Admin", policy => policy.RequireRole("1")); // Role 1 - Admin

    // Policy for NhanVien role
    options.AddPolicy("Nhân Viên", policy => policy.RequireRole("2")); // Role 2 - NhanVien

    // Policy for KhachHang role
    options.AddPolicy("Khách Hàng", policy => policy.RequireRole("3")); // Role 3 - KhachHang
});

// ========================
// APPLICATION CONFIGURATION
// ========================
var app = builder.Build();

// ========================
// MIDDLEWARE PIPELINE
// ========================
// Exception handling - should be first in pipeline
app.UseMiddleware<ExceptionMiddleware>();

// Enable CORS
app.UseCors("AllowAll");

// Development environment configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Web API v1");
        c.RoutePrefix = string.Empty;
    });
}
else
{
    // In production, always use Swagger
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Web API v1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at the root URL
    });
}

// Security and routing middleware
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Endpoints
app.MapControllers();

// ========================
// RUN APPLICATION
// ========================
app.Run();

// Configure Data Protection to use environment variables
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/tmp/keys"))
    .SetApplicationName("MyWebApi")
    .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
    {
        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
    });

// Configure HTTPS redirection
builder.Services.AddHttpsRedirection(options =>
{
    // Render.com handles HTTPS termination, so we don't need to specify a port
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
});