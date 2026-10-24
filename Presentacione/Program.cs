using Datos.Interfaces;
using Datos.Services;
using Infractructure.Repositori;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Repositorios Singleton (persistencia en memoria para el prototipo interactivo)
builder.Services.AddSingleton<IProducto, ProductoRepository>();
builder.Services.AddSingleton<IEntradaMercancia, EntradaMercanciaRepository>();
builder.Services.AddSingleton<ISalidaMercancia, SalidaMercanciaRepository>();

// Servicios de aplicacion
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<EntradaMercanciaService>();
builder.Services.AddScoped<SalidaMercanciaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
