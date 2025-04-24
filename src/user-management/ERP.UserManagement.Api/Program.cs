using ERP.UserManagement.Api.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<UserManagementContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserMangement"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "User Management", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

// Auth Endpoints
app.MapPost("/token", async ([FromBody] LoginRequest request, UserManagementContext db) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
    if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
    {
        return Results.BadRequest("Invalid credentials");
    }

    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity([
            new Claim(ClaimTypes.Name, user.Username)
        ]),
        Expires = DateTime.UtcNow.AddMinutes(30),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    var jwtToken = tokenHandler.WriteToken(token);

    return Results.Ok(new TokenResponse(jwtToken, "bearer"));
});

// Protected User Info
app.MapGet("/users/me", [Authorize] async (HttpContext httpContext, UserManagementContext db) =>
{
    var username = httpContext.User.Identity?.Name;
    var user = await db.Users.FirstOrDefaultAsync(u => u.Username == username);
    if (user is null) return Results.NotFound();

    var roleIds = await db.UserRoles
        .Where(ur => ur.UserId == user.UserId)
        .Select(ur => ur.RoleId).ToListAsync();

    var roles = await db.Roles
        .Where(r => roleIds.Contains(r.RoleId))
        .Select(r => r.RoleName).ToListAsync();

    return Results.Ok(new UserResponse(user.Username, user.Email, roles));
});

app.MapGet("/users/me/roles", [Authorize] async (HttpContext httpContext, UserManagementContext db) =>
{
    var username = httpContext.User.Identity?.Name;
    var user = await db.Users.FirstOrDefaultAsync(u => u.Username == username);
    if (user is null) return Results.NotFound();

    var roleIds = await db.UserRoles
        .Where(ur => ur.UserId == user.UserId)
        .Select(ur => ur.RoleId).ToListAsync();

    var roles = await db.Roles
        .Where(r => roleIds.Contains(r.RoleId))
        .Select(r => r.RoleName).ToListAsync();

    return Results.Ok(roles);
});

app.Run();

// DTOs
record LoginRequest(string Username, string Password);
record TokenResponse(string AccessToken, string TokenType);
record UserResponse(string Username, string Email, List<string> Roles);
