using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ChuoiKetNoi")));
//builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// nếu cần đặt lại mật khẩu, xác minh email, xác thực 2 yếu tố thì cần thêm AddDefaultTokenProviders vào
//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//  .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
  .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
  options.AccessDeniedPath = "/Account/AccessDenied";
  options.LogoutPath = "/Account/Login";
});

builder.Services.Configure<IdentityOptions>(options =>
{
  options.Password.RequiredLength = 6;
});

var app = builder.Build();

// Stripe tên hệ thống thanh toán (dùng nuget package)
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
