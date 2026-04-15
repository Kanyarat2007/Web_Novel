using Microsoft.EntityFrameworkCore;
using Web_Novel.Models;
using Web_Novel.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WebnoveldbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<RewardService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Login นอก Area
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

// Route สำหรับ Area
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Route หน้าแรก → Reader/Home/Index
app.MapControllerRoute(
    name: "default",
    pattern: "",
    defaults: new { area = "Reader", controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "default_outside_area",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();