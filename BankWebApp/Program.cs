using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BankWebApp.Data;
using BankWebApp.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BankWebAppContextConnection") ?? throw new InvalidOperationException("Connection string 'BankWebAppContextConnection' not found.");

builder.Services.AddDbContext<BankWebAppContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<BankWebAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BankWebAppContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
