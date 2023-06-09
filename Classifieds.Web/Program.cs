using Classifieds.Data;
using Classifieds.Data.Entities;
using Classifieds.Web.Services;
using Classifieds.Web.Services.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddDefaultIdentity<User>(option =>
    {
        option.Password.RequireDigit = true;
        option.Password.RequireLowercase = true;
        option.Password.RequireUppercase = true;
        option.Password.RequiredLength = 7;
        option.SignIn.RequireConfirmedAccount = false;

        option.Lockout.AllowedForNewUsers=true;
        option.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(10);
        option.Lockout.MaxFailedAccessAttempts = 5;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddPasswordValidator<PasswordValidator>()
    .AddClaimsPrincipalFactory<CustomClaimsService>();

builder.Services.AddAuthentication()
    .AddCookie(q => q.LoginPath = "/Auth/Login")
    .AddGoogle(o => { o.ClientId = builder.Configuration["Google:ClientId"]; o.ClientSecret = builder.Configuration["Google:ClientSecret"]; });
//////.AddTwitter(o => { o.ConsumerKey = ""; o.ConsumerSecret = ""; })
//////.AddFacebook(o => { o.ClientId = ""; o.ClientSecret = ""; })
//////.AddMicrosoftAccount(o => { o.ClientId = ""; o.ClientSecret = ""; });

builder.Services.AddTransient<IEmailSender, EmailSender>(s=>new EmailSender("Localhost",25,"sssss@gmail.com"));

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});
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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
