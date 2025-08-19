using administracionScoutsCR.Controllers;
using administracionScoutsCR.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);
var defaultCulture = new CultureInfo("es-ES");
var localizationOptions = new RequestLocalizationOptions
{
	DefaultRequestCulture = new RequestCulture(defaultCulture),
	SupportedCultures = new List<CultureInfo> { defaultCulture },
	SupportedUICultures = new List<CultureInfo> { defaultCulture }
};

// Add services to the container
builder.Services.AddDbContext<DatabaseScoutContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<RecordatorioEventosService>();

// Configuración de autenticación por cookies (solo UNA vez)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/ControllerLogin/Login"; // Redirección al login si no está autenticado
		options.AccessDeniedPath = "/Home/Index"; // Redirección si no tiene permisos
	});

// Autorización global: requiere login para toda la app por defecto
builder.Services.AddControllersWithViews(options =>
{
	options.Filters.Add(new AuthorizeFilter());
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
