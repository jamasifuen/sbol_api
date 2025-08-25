using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PersonalAPI.Data;
using PersonalAPI.Models.Auth;
using PersonalAPI.Services;
using PersonalAPI.Services.Auth;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Construir cadena de conexión usando variables de entorno
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Si estamos en Docker/Producción, usar variables de entorno
if (builder.Environment.IsProduction() || !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DB_SERVER")))
{
    var dbServer = Environment.GetEnvironmentVariable("DB_SERVER") ?? "host.docker.internal,1433";
    var dbDatabase = Environment.GetEnvironmentVariable("DB_DATABASE") ?? "SIMACHDB";
    var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "api_personal";
    var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "api_personal";
    
    connectionString = $"Server={dbServer};Database={dbDatabase};User Id={dbUser};Password={dbPassword};TrustServerCertificate=true;Encrypt=false;";
}

// Configurar Entity Framework
builder.Services.AddDbContext<PersonalContext>(options =>
    options.UseSqlServer(connectionString));

// Configurar JWT usando variables de entorno
var jwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? 
                   builder.Configuration["JwtSettings:SecretKey"] ?? 
                   "PersonalAPI_SuperSecretKey_MinimumLengthRequired_ForJWT_Signature_2024";

var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? 
                builder.Configuration["JwtSettings:Issuer"] ?? 
                "PersonalAPI";

var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? 
                  builder.Configuration["JwtSettings:Audience"] ?? 
                  "PersonalAPI_Users";

var jwtExpiryMinutes = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRY_MINUTES") ?? 
                                builder.Configuration["JwtSettings:ExpiryInMinutes"] ?? 
                                "60");

// Crear configuración JWT
var jwtSettings = new JwtSettings
{
    SecretKey = jwtSecretKey,
    Issuer = jwtIssuer,
    Audience = jwtAudience,
    ExpiryInMinutes = jwtExpiryMinutes
};

builder.Services.Configure<JwtSettings>(options =>
{
    options.SecretKey = jwtSettings.SecretKey;
    options.Issuer = jwtSettings.Issuer;
    options.Audience = jwtSettings.Audience;
    options.ExpiryInMinutes = jwtSettings.ExpiryInMinutes;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Para desarrollo
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
        ClockSkew = TimeSpan.Zero,
        // Configuraciones adicionales para mayor compatibilidad
        RequireExpirationTime = true,
        RequireSignedTokens = true
    };

    // Agregar logging para debugging
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token validated successfully");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine($"OnChallenge error: {context.Error}, {context.ErrorDescription}");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

// Registrar servicios
builder.Services.AddScoped<IPersonalService, PersonalService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
    
    options.AddPolicy("Development", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173", "http://127.0.0.1:5500")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "SBOL API", 
        Version = "v1",
        Description = "API para gestión de personal con autenticación JWT"
    });

    // Configurar autenticación JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando el esquema Bearer. Ejemplo: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Personal API v1");
        c.DocumentTitle = "Personal API - Swagger UI";
    });
}
else
{
    // En producción/Docker también habilitar Swagger
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Personal API v1");
        c.DocumentTitle = "Personal API - Swagger UI";
    });
}

// Solo redirigir HTTPS si no estamos en Docker o si está configurado HTTPS
var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true" || 
               Environment.GetEnvironmentVariable("ASPNETCORE_URLS")?.Contains("https") == true;

if (!isDocker)
{
    app.UseHttpsRedirection();
}

// Habilitar CORS
app.UseCors("AllowAll");

// Importante: El orden es crítico
app.UseAuthentication();  // Debe ir antes de UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();
