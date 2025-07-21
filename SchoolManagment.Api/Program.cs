using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolManagment.Core;
using SchoolManagment.Core.Middleware;
using SchoolManagment.Infrastructure;
using SchoolManagment.Infrastructure.Data;
using SchoolManagment.Service;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle  
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// extension methods for adding dependencies
builder.Services.AddInfrastructureDependencies().AddModuleServiceDependencies().AddModuleCoreDependencies();

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

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
