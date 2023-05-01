using Highscore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Highscore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services
  .AddDefaultIdentity<ApplicationUser>()
  .AddEntityFrameworkStores<ApplicationContext>();
builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
});

//builder.Services.AddMvcCore(options =>
//{
//    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
//    options.Filters.Add(new AuthorizeFilter(policy));
//}).AddXmlSerializerFormatters();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "games",
    pattern: "games/{urlSlug}",
    defaults: new { Controller = "Games", Action = "Details" });
// MVC-modell
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Razor Pages-modell
app.MapRazorPages();

app.Run();
