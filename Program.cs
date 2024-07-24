using Microsoft.AspNetCore.Authentication.Cookies;
using Progetto_Settimana_2_Manuel.DAO;
using Progetto_Settimana_2_Manuel.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        // pagina alla quale l'utente sarà indirizzato se non è stato già riconosciuto
        opt.LoginPath = "/Auth/Login";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});



builder.Services
    .AddScoped<SqlServerServiceBase>();
builder.Services.AddScoped<IClienteDAO, ClienteDAO>()
.AddScoped<ICameraDAO, CameraDAO>()
.AddScoped<IPrenotazioneDAO, PrenotazioneDAO>()
.AddScoped<IServizioDAO, ServizioDAO>()
.AddScoped<IPrenotazioneServizioDAO, PrenotazioneServizioDAO>()
.AddScoped<IAuthService, AuthService>()
.AddScoped<IPasswordEncoder, PasswordEncoder>();    
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
