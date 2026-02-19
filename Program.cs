using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// 2. Configuración de CORS (Ajustado para producción)
builder.Services.AddCors(options => 
    options.AddDefaultPolicy(policy => 
        policy.AllowAnyOrigin() // En el futuro cambia esto por la URL de tu Vercel
              .AllowAnyMethod()
              .AllowAnyHeader()));

// 3. Conexión a Supabase (PostgreSQL)
// Busca la conexión en appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(connectionString));

var app = builder.Build();

app.UseCors();

// --- INICIALIZACIÓN DE BASE DE DATOS (MÉTODO SUPABASE/POSTGRES) ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Solo verificamos la conexión, ya no intentamos crear ni borrar desde aquí
    try {
        db.Database.CanConnect();
        Console.WriteLine("🚀 Conexión exitosa con Supabase");
    } catch (Exception ex) {
        Console.WriteLine($"❌ Error de conexión: {ex.Message}");
    }
}
// --- ENDPOINTS DE AUTENTICACIÓN ---
app.MapPost("/api/login", async (LoginRequest login, AppDbContext db) =>
{
    var user = await db.Usuarios
        .FirstOrDefaultAsync(u => u.NombreUsuario == login.Usuario && u.Contrasena == login.Contrasena);

    if (user is null)
    {
        return Results.Json(new { message = "Usuario o contraseña incorrectos" }, statusCode: 401);
    }

    return Results.Ok(new 
    { 
        id = user.Id, 
        usuario = user.NombreUsuario,
        success = true 
    });
});

app.MapArtesanoEndpoints();
app.MapArtesanoSitiEndpoints();
app.MapArtesanoCNEndpoints();
app.MapArtesanoRPEndpoints();

app.Run();