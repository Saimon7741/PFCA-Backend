using Microsoft.EntityFrameworkCore;

public static class ArtesanoEndpoints
{
    public static void MapArtesanoEndpoints(this WebApplication app)
    {
        app.MapGet("/api/artesanos", async (AppDbContext db) => 
            await db.Artesanos.ToListAsync());

        app.MapGet("/api/artesanos/{id:int}", async (int id, AppDbContext db) =>
            await db.Artesanos.FindAsync(id) is Artesano artesano 
                ? Results.Ok(artesano) 
                : Results.NotFound());

        app.MapPost("/api/artesanos", async (Artesano newArtesano, AppDbContext db) =>
        {
            newArtesano.CalcularTotalParticipaciones();
            db.Artesanos.Add(newArtesano);
            await db.SaveChangesAsync();
            return Results.Created($"/api/artesanos/{newArtesano.Id}", newArtesano);
        });

        app.MapPut("/api/artesanos/{id:int}", async (int id, Artesano updatedArtesano, AppDbContext db) =>
        {
            // 1. Buscar el registro real en la DB
            var artesano = await db.Artesanos.FindAsync(id);
            if (artesano is null) return Results.NotFound($"No se encontró el artesano con ID {id}");

            // 2. Mapeo manual (Aseguramos que no se pierdan datos)
            artesano.Cuadrante = updatedArtesano.Cuadrante;
            artesano.Toldo = updatedArtesano.Toldo;
            artesano.DocumentoIdentidad = updatedArtesano.DocumentoIdentidad;
            artesano.Nombre = updatedArtesano.Nombre;
            artesano.Producto = updatedArtesano.Producto;
            artesano.Marca = updatedArtesano.Marca;
            artesano.Sexo = updatedArtesano.Sexo;
            artesano.FechaNacimiento = updatedArtesano.FechaNacimiento;
            artesano.Telefono = updatedArtesano.Telefono;
            artesano.Observaciones = updatedArtesano.Observaciones;
            
            // Meses
            artesano.Enero = updatedArtesano.Enero;
            artesano.Febrero = updatedArtesano.Febrero;
            artesano.Marzo = updatedArtesano.Marzo;
            artesano.Abril = updatedArtesano.Abril;
            artesano.Mayo = updatedArtesano.Mayo;
            artesano.Junio = updatedArtesano.Junio;
            artesano.Julio = updatedArtesano.Julio;
            artesano.Agosto = updatedArtesano.Agosto;
            artesano.Septiembre = updatedArtesano.Septiembre;
            artesano.Octubre = updatedArtesano.Octubre;
            artesano.Noviembre = updatedArtesano.Noviembre;
            artesano.Diciembre = updatedArtesano.Diciembre;
            
            artesano.CalcularTotalParticipaciones();

            try 
            {
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                // Esto evita que el error bloquee futuras peticiones
                Console.WriteLine($"Error al actualizar ID {id}: {ex.Message}");
                return Results.Problem("Error al guardar en base de datos. Verifique los formatos de fecha o campos obligatorios.");
            }
        });

        app.MapDelete("/api/artesanos/{id:int}", async (int id, AppDbContext db) =>
        {
            if (await db.Artesanos.FindAsync(id) is Artesano artesano)
            {
                db.Artesanos.Remove(artesano);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        });
    }
}