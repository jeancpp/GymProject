using GymProject.Data;
using GymProject.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<GymDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("GymDbConnectionString")));

builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("GymAuthDbConnectionString")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();
builder.Services.Configure<IdentityOptions>(options => { 
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;


});
builder.Services.AddScoped<ICategoriasRepository, CategoriasRepository>();
builder.Services.AddScoped<IEjerciciosRepository, EjerciciosRepository>();
builder.Services.AddScoped<IRutinasRepository, RutinasRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Login";  // Ahora redirige a LoginController en lugar de AccountController
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Login/AccessDenied"; // Ajusta según tu LoginController
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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
