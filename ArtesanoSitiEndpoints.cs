using Microsoft.EntityFrameworkCore;

public static class ArtesanoSitiEndpoints
{
    public static void MapArtesanoSitiEndpoints(this WebApplication app)
    {
        // Endpoints para ArtesanosSiti
        app.MapGet("/api/artesanos-siti", async (AppDbContext db) => 
            await db.ArtesanosSiti.ToListAsync());

        app.MapGet("/api/artesanos-siti/{id:int}", async (int id, AppDbContext db) =>
            await db.ArtesanosSiti.FindAsync(id) is ArtesanoSiti artesanoSiti 
                ? Results.Ok(artesanoSiti) 
                : Results.NotFound());

        app.MapPost("/api/artesanos-siti", async (ArtesanoSiti newArtesanoSiti, AppDbContext db) =>
        {
            newArtesanoSiti.CalcularTotalParticipaciones();
            db.ArtesanosSiti.Add(newArtesanoSiti);
            await db.SaveChangesAsync();
            return Results.Created($"/api/artesanos-siti/{newArtesanoSiti.Id}", newArtesanoSiti);
        });

        app.MapPut("/api/artesanos-siti/{id:int}", async (int id, ArtesanoSiti updatedArtesanoSiti, AppDbContext db) =>
        {
            var artesanoSiti = await db.ArtesanosSiti.FindAsync(id);
            if (artesanoSiti is null) return Results.NotFound();

            // Actualizar propiedades
            artesanoSiti.FechaIngreso = updatedArtesanoSiti.FechaIngreso;
            artesanoSiti.TipoIdentificacion = updatedArtesanoSiti.TipoIdentificacion;
            artesanoSiti.DocumentoIdentidad = updatedArtesanoSiti.DocumentoIdentidad;
            artesanoSiti.Nombre = updatedArtesanoSiti.Nombre;
            artesanoSiti.Producto = updatedArtesanoSiti.Producto;
            artesanoSiti.Marca = updatedArtesanoSiti.Marca;
            artesanoSiti.Telefono = updatedArtesanoSiti.Telefono;
            artesanoSiti.Correo = updatedArtesanoSiti.Correo;
            artesanoSiti.Sexo = updatedArtesanoSiti.Sexo;
            artesanoSiti.FechaNacimiento = updatedArtesanoSiti.FechaNacimiento;
            artesanoSiti.Observaciones = updatedArtesanoSiti.Observaciones;
            
            // Actualizar meses
            artesanoSiti.Enero = updatedArtesanoSiti.Enero;
            artesanoSiti.Febrero = updatedArtesanoSiti.Febrero;
            artesanoSiti.Marzo = updatedArtesanoSiti.Marzo;
            artesanoSiti.Abril = updatedArtesanoSiti.Abril;
            artesanoSiti.Mayo = updatedArtesanoSiti.Mayo;
            artesanoSiti.Junio = updatedArtesanoSiti.Junio;
            artesanoSiti.Julio = updatedArtesanoSiti.Julio;
            artesanoSiti.Agosto = updatedArtesanoSiti.Agosto;
            artesanoSiti.Septiembre = updatedArtesanoSiti.Septiembre;
            artesanoSiti.Octubre = updatedArtesanoSiti.Octubre;
            artesanoSiti.Noviembre = updatedArtesanoSiti.Noviembre;
            artesanoSiti.Diciembre = updatedArtesanoSiti.Diciembre;
            
            artesanoSiti.CalcularTotalParticipaciones();
            
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        app.MapDelete("/api/artesanos-siti/{id:int}", async (int id, AppDbContext db) =>
        {
            if (await db.ArtesanosSiti.FindAsync(id) is ArtesanoSiti artesanoSiti)
            {
                db.ArtesanosSiti.Remove(artesanoSiti);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        });
    }
}