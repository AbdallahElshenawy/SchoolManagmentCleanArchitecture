using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolManagment.Core;
using SchoolManagment.Core.Middleware;
using SchoolManagment.Data.Entities.Identity;
using SchoolManagment.Data.Helper;
using SchoolManagment.Infrastructure;
using SchoolManagment.Infrastructure.Data;
using SchoolManagment.Infrastructure.Seeder;
using SchoolManagment.Infrastructure.Seeder.UserSeeder;
using SchoolManagment.Service;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle  
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "School Management API",
        Description = "School Management System Web API"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid JWT token "
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// extension methods for adding dependencies
builder.Services.AddInfrastructureDependencies().AddModuleServiceDependencies().AddModuleCoreDependencies();
builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
// Policy for authorization
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("CreateStudent", policy =>
    {
        policy.RequireClaim("Create Student", "True");
    });
    option.AddPolicy("DeleteStudent", policy =>
    {
        policy.RequireClaim("Delete Student", "True");
    });
    option.AddPolicy("EditStudent", policy =>
    {
        policy.RequireClaim("Edit Student", "True");
    });
});
#region JWT
var jwtSettings = builder.Configuration.GetSection("jwtSettings").Get<JwtSettings>();
if (jwtSettings != null)
{
    builder.Services.AddSingleton(jwtSettings);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = jwtSettings.ValidateIssuer,
            ValidateAudience = jwtSettings.ValidateAudience,
            ValidateLifetime = jwtSettings.ValidateLifeTime,
            ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
            ValidAudience = jwtSettings.Audience,
            ValidIssuer = jwtSettings.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            ClockSkew = TimeSpan.Zero
        };
    });
}

#endregion

#region Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"), new CultureInfo("ar-EG")
    };
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
#endregion
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleSeeder.SeedAsync(roleManager);
    await UserSeeder.SeedAsync(userManager);
}

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "School Project v1");
    });
}
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
