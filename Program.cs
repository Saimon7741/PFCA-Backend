using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.OpenApi.Models; // Necesario para Swagger

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// --- SERVICIOS ---

// 1. Configurar JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// 2. NUEVO: Registrar Swagger (Faltaba esto)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Artesanos", Version = "v1" });
});

// 3. Configuración de CORS
builder.Services.AddCors(options => 
    options.AddDefaultPolicy(policy => 
        policy.AllowAnyOrigin() 
              .AllowAnyMethod()
              .AllowAnyHeader()));

// 4. Conexión a Supabase
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(connectionString));

var app = builder.Build();

// --- MIDDLEWARE ---

app.UseCors();

// Siempre habilitar Swagger (Incluso en producción/Render)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Artesanos v1");
    c.RoutePrefix = string.Empty; // Swagger en la raíz: https://tu-app.onrender.com/
});

// --- VERIFICACIÓN DE CONEXIÓN ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try {
        if (db.Database.CanConnect()) {
            Console.WriteLine("🚀 Conexión exitosa con Supabase");
        }
    } catch (Exception ex) {
        Console.WriteLine($"❌ Error de conexión: {ex.Message}");
    }
}

// --- ENDPOINTS ---

// Login
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

// Map de tus otros archivos
app.MapArtesanoEndpoints();
app.MapArtesanoSitiEndpoints();
app.MapArtesanoCNEndpoints();
app.MapArtesanoRPEndpoints();

app.Run();