using Microsoft.EntityFrameworkCore;
using UTB.BaChr.Mapy.Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.Cookies; // NOVÉ: Pøidat tento namespace

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// NOVÉ: Konfigurace Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });
// KONEC NOVÉHO

string connectionString = builder.Configuration.GetConnectionString("MySQL");
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

app.UseAuthentication(); // NOVÉ: Musí být PØED UseAuthorization
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();