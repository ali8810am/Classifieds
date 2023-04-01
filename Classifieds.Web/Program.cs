using Classifieds.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddMvcOptions(q => q.Filters.Add(new AuthorizeFilter()));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));
builder.Services.AddAuthentication(o =>
    {
        o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        ////o.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
    })
    .AddCookie(q => q.LoginPath = "/Auth/Login")
    .AddGoogle(o => { o.ClientId = builder.Configuration["Google:ClientId"]; o.ClientSecret = builder.Configuration["Google:ClientSecret"]; });
////.AddTwitter(o => { o.ConsumerKey = ""; o.ConsumerSecret = ""; })
////.AddFacebook(o => { o.ClientId = ""; o.ClientSecret = ""; })
////.AddMicrosoftAccount(o => { o.ClientId = ""; o.ClientSecret = ""; });
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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
