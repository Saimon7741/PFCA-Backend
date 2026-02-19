using Microsoft.EntityFrameworkCore;

public static class ArtesanoCNEndpoints
{
    public static void MapArtesanoCNEndpoints(this WebApplication app)
    {
        // Endpoints para ArtesanosCN
        app.MapGet("/api/artesanos-cn", async (AppDbContext db) => 
            await db.ArtesanosCN.ToListAsync());

        app.MapGet("/api/artesanos-cn/{id:int}", async (int id, AppDbContext db) =>
            await db.ArtesanosCN.FindAsync(id) is ArtesanoCN artesanoCN 
                ? Results.Ok(artesanoCN) 
                : Results.NotFound());

        app.MapPost("/api/artesanos-cn", async (ArtesanoCN newArtesanoCN, AppDbContext db) =>
        {
            newArtesanoCN.CalcularTotal();
            db.ArtesanosCN.Add(newArtesanoCN);
            await db.SaveChangesAsync();
            return Results.Created($"/api/artesanos-cn/{newArtesanoCN.Id}", newArtesanoCN);
        });

        app.MapPut("/api/artesanos-cn/{id:int}", async (int id, ArtesanoCN updatedArtesanoCN, AppDbContext db) =>
        {
            var artesanoCN = await db.ArtesanosCN.FindAsync(id);
            if (artesanoCN is null) return Results.NotFound();

            // Actualizar propiedades
            artesanoCN.FechaIngreso = updatedArtesanoCN.FechaIngreso;
            artesanoCN.TipoIdentificacion = updatedArtesanoCN.TipoIdentificacion;
            artesanoCN.DocumentoIdentidad = updatedArtesanoCN.DocumentoIdentidad;
            artesanoCN.Nombre = updatedArtesanoCN.Nombre;
            artesanoCN.Producto = updatedArtesanoCN.Producto;
            artesanoCN.Marca = updatedArtesanoCN.Marca;
            artesanoCN.Sexo = updatedArtesanoCN.Sexo;
            artesanoCN.Telefono = updatedArtesanoCN.Telefono;
            artesanoCN.Correo = updatedArtesanoCN.Correo;
            artesanoCN.FechaNacimiento = updatedArtesanoCN.FechaNacimiento;
            artesanoCN.Observaciones = updatedArtesanoCN.Observaciones;
            
            // Actualizar meses y eventos
            artesanoCN.Enero = updatedArtesanoCN.Enero;
            artesanoCN.Febrero = updatedArtesanoCN.Febrero;
            artesanoCN.Marzo = updatedArtesanoCN.Marzo;
            artesanoCN.Abril = updatedArtesanoCN.Abril;
            artesanoCN.Mayo = updatedArtesanoCN.Mayo;
            artesanoCN.Junio = updatedArtesanoCN.Junio;
            artesanoCN.Julio = updatedArtesanoCN.Julio;
            artesanoCN.Agosto = updatedArtesanoCN.Agosto;
            artesanoCN.Septiembre = updatedArtesanoCN.Septiembre;
            artesanoCN.Octubre = updatedArtesanoCN.Octubre;
            artesanoCN.Noviembre = updatedArtesanoCN.Noviembre;
            artesanoCN.Diciembre = updatedArtesanoCN.Diciembre;
            
            artesanoCN.CalcularTotal();
            
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        app.MapDelete("/api/artesanos-cn/{id:int}", async (int id, AppDbContext db) =>
        {
            if (await db.ArtesanosCN.FindAsync(id) is ArtesanoCN artesanoCN)
            {
                db.ArtesanosCN.Remove(artesanoCN);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        });
    }
}