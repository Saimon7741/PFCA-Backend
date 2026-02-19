[Table("ArtesanosCN")]
public class ArtesanoCN
{
    [Column("Id")] public int Id { get; set; }
    [Column("FechaIngreso")] public DateTime? FechaIngreso { get; set; }
    [Column("TipoIdentificacion")] public string? TipoIdentificacion { get; set; }
    [Column("DocumentoIdentidad")] public string? DocumentoIdentidad { get; set; }
    [Column("Nombre")] public string? Nombre { get; set; }
    [Column("Producto")] public string? Producto { get; set; }
    [Column("Marca")] public string? Marca { get; set; }
    [Column("Sexo")] public string? Sexo { get; set; }
    [Column("Telefono")] public string? Telefono { get; set; }
    [Column("Correo")] public string? Correo { get; set; }
    [Column("FechaNacimiento")] public DateTime? FechaNacimiento { get; set; }
    [Column("Observaciones")] public string? Observaciones { get; set; }
    [Column("Enero")] public bool Enero { get; set; }
    [Column("Febrero")] public bool Febrero { get; set; }
    [Column("Marzo")] public bool Marzo { get; set; }
    [Column("Abril")] public bool Abril { get; set; }
    [Column("Mayo")] public bool Mayo { get; set; }
    [Column("Junio")] public bool Junio { get; set; }
    [Column("Julio")] public bool Julio { get; set; }
    [Column("Agosto")] public bool Agosto { get; set; }
    [Column("Septiembre")] public bool Septiembre { get; set; }
    [Column("Octubre")] public bool Octubre { get; set; }
    [Column("Noviembre")] public bool Noviembre { get; set; }
    [Column("Diciembre")] public bool Diciembre { get; set; }
    
    [Column("Total")] // En tu SQL se llama Total
    public int Total { get; set; }

        public void CalcularTotalParticipaciones()
    {
        Total = (Enero ? 1 : 0) + (Febrero ? 1 : 0) + (Marzo ? 1 : 0) + 
                               (Abril ? 1 : 0) + (Mayo ? 1 : 0) + (Junio ? 1 : 0) + 
                               (Julio ? 1 : 0) + (Agosto ? 1 : 0) + (Septiembre ? 1 : 0) + 
                               (Octubre ? 1 : 0) + (Noviembre ? 1 : 0) + (Diciembre ? 1 : 0);
    }
}