using Microsoft.EntityFrameworkCore;

public static class ArtesanoRPEndpoints
{
    public static void MapArtesanoRPEndpoints(this WebApplication app)
    {
        // Obtener todos
        app.MapGet("/api/artesanos-rp", async (AppDbContext db) => 
            await db.ArtesanosRP.ToListAsync());

        // Obtener por ID
        app.MapGet("/api/artesanos-rp/{id:int}", async (int id, AppDbContext db) =>
            await db.ArtesanosRP.FindAsync(id) is ArtesanoRP artesano
                ? Results.Ok(artesano) 
                : Results.NotFound());

        // Crear
        app.MapPost("/api/artesanos-rp", async (ArtesanoRP newArtesano, AppDbContext db) =>
        {
            db.ArtesanosRP.Add(newArtesano);
            await db.SaveChangesAsync();
            return Results.Created($"/api/artesanos-rp/{newArtesano.Id}", newArtesano);
        });

        // Actualizar
        app.MapPut("/api/artesanos-rp/{id:int}", async (int id, ArtesanoRP updatedArtesano, AppDbContext db) =>
        {
            var artesano = await db.ArtesanosRP.FindAsync(id);
            if (artesano is null) return Results.NotFound();

            artesano.FechaRevision = updatedArtesano.FechaRevision;
            artesano.TipoDocumento = updatedArtesano.TipoDocumento;
            artesano.NumeroDocumento = updatedArtesano.NumeroDocumento;
            artesano.Nombre = updatedArtesano.Nombre;
            artesano.Taller = updatedArtesano.Taller;
            artesano.PuntajeTotal = updatedArtesano.PuntajeTotal;
            artesano.Resultado = updatedArtesano.Resultado;
            artesano.Link = updatedArtesano.Link;
            
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        // Eliminar
        app.MapDelete("/api/artesanos-rp/{id:int}", async (int id, AppDbContext db) =>
        {
            if (await db.ArtesanosRP.FindAsync(id) is ArtesanoRP artesano)
            {
                db.ArtesanosRP.Remove(artesano);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        });
    }
}