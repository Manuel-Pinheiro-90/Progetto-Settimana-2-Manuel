using Progetto_Settimana_2_Manuel.DAO;
using Progetto_Settimana_2_Manuel.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddScoped<SqlServerServiceBase>();
    builder.Services.AddScoped<IClienteDAO, ClienteDAO>();
builder.Services.AddScoped<ICameraDAO, CameraDAO>();    
builder.Services.AddScoped<IPrenotazioneDAO, PrenotazioneDAO>();
builder.Services.AddScoped<IServizioDAO, ServizioDAO>();
builder.Services.AddScoped<IPrenotazioneServizioDAO, PrenotazioneServizioDAO>();


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
