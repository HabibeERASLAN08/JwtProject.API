using JwtProject.Web.Handlers;
using JwtProject.Web.Options;
using JwtProject.Web.Services.Categories;
using JwtProject.Web.Services.Tokens;
using JwtProject.Web.Services.Users;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<JwtTokenHandler>();

builder.Services.Configure<ApiOptions>(builder.Configuration.GetSection(nameof(ApiOptions)));

var apiOptions=builder.Configuration.GetSection(nameof(ApiOptions)).Get<ApiOptions>();

builder.Services.AddHttpClient<IUserService, UserService>(options =>
{
    options.BaseAddress = new Uri(apiOptions.BaseAddress);
});
builder.Services.AddHttpClient<ICategoryService, CategoryService>(options =>
{
    options.BaseAddress = new Uri(apiOptions.BaseAddress);
}).AddHttpMessageHandler<JwtTokenHandler>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,options=>

{
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
    options.Cookie.Name = "JwtProjectCookie";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.SameSite=SameSiteMode.Strict;
});
builder.Services.AddControllersWithViews();

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
