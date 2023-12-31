using Habanero.Util;
using InstituteManagement.Models.Interfaces;
using InstituteManagement.Models.Repositories;
using InstituteManagement_Models;
using InstituteManagement_Models.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connstr = builder.Configuration.GetConnectionString("conn");
builder.Services.AddDbContext<AppDbContext>(Options => Options.UseSqlServer(connstr));

//for desiging
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedEmail=true)
  .AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IUserAccountConfirm, UserAccountConfirmRepo>();
builder.Services.AddScoped<ISubscriptionRepo, SubscriptionRepo>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
