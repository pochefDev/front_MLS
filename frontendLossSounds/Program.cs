using frontendLossSounds.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrar IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Agregar el servicio de sesi�n
builder.Services.AddSession(options =>
{
    // Configuraci�n de opciones de sesi�n
    options.Cookie.Name = ".MySession";
    options.IdleTimeout = TimeSpan.FromMinutes(1440); // Tiempo de expiraci�n de sesi�n
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Habilitar el uso de sesiones
app.UseSession();

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
