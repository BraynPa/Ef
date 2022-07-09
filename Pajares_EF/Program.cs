using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Pajares_EF.BD;
using Pajares_EF.Repository;
using Pajares_EF.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.LoginPath = "/auth/login";
    });

// Configure the HTTP request pipeline.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EFContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"))
);
builder.Services.AddTransient<ICookieAuthService, CookieAuthService>();
builder.Services.AddTransient<IEFContext, EFContext>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<ICuentaRepository, CuentaRepository>();

var app = builder.Build();
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
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
