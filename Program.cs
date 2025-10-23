using GovScedulaTrial.Models.Data.Services;
using GovSchedulaWeb.Models.Data.GovSchedulaDBContext;
using GovSchedulaWeb.Models.Data.Services;
using GovSchedulaWeb.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Configuration
       .AddJsonFile("api.json", optional: false, reloadOnChange: true);
builder.Services.AddDbContext<GovSchedulaDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("GovSchedulaDBConnection"));
});
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<PassportService>();
builder.Services.AddScoped<VoterRegService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddSession();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
