using Microsoft.EntityFrameworkCore;
using UTB.BaChr.Mapy.Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using UTB.BaChr.Mapy.Application.Abstraction;
using UTB.BaChr.Mapy.Application.Implementation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Konfigurace Cookie Authentication (zachováno z vašeho kódu)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// DÙLEŽITÉ: Registrace služby pro lokace (aby fungovala mapa v HomeControlleru)
builder.Services.AddScoped<ILocationService, LocationService>();

string connectionString = builder.Configuration.GetConnectionString("MySQL");
// Ujistìte se, že verze serveru odpovídá vaší databázi (zachováno z vašeho kódu)
ServerVersion serverVersion = new MySqlServerVersion("8.0.38");

builder.Services.AddDbContext<MapyDbContext>(optionsBuilder => optionsBuilder.UseMySql(connectionString, serverVersion));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();