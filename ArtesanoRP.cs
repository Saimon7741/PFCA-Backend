using System.ComponentModel.DataAnnotations.Schema;

[Table("ArtesanosRP")]
public class ArtesanoRP
{
    [Column("Id")] public int Id { get; set; }
    [Column("FechaRevision")] public DateTime FechaRevision { get; set; } // Sin ? porque es NOT NULL
    [Column("TipoDocumento")] public string? TipoDocumento { get; set; }
    [Column("NumeroDocumento")] public string? NumeroDocumento { get; set; }
    [Column("Nombre")] public string? Nombre { get; set; }
    [Column("Taller")] public string? Taller { get; set; }
    [Column("PuntajeTotal")] public double PuntajeTotal { get; set; } // Sin ? porque es NOT NULL
    [Column("Resultado")] public string? Resultado { get; set; }
    [Column("Link")] public string? Link { get; set; }
}