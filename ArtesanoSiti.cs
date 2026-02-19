using System;

public class ArtesanoSiti
{
    public int Id { get; set; }
    public DateTime? FechaIngreso { get; set; }
    public string? TipoIdentificacion { get; set; }
    public string? DocumentoIdentidad { get; set; }
    public string? Nombre { get; set; }
    public string? Producto { get; set; }
    public string? Marca { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public string? Sexo { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string? Observaciones { get; set; }
    
    public bool Enero { get; set; }
    public bool Febrero { get; set; }
    public bool Marzo { get; set; }
    public bool Abril { get; set; }
    public bool Mayo { get; set; }
    public bool Junio { get; set; }
    public bool Julio { get; set; }
    public bool Agosto { get; set; }
    public bool Septiembre { get; set; }
    public bool Octubre { get; set; }
    public bool Noviembre { get; set; }
    public bool Diciembre { get; set; }
    
    public int TotalParticipaciones { get; set; }

    public void CalcularTotalParticipaciones()
    {
        TotalParticipaciones = (Enero ? 1 : 0) + (Febrero ? 1 : 0) + 
                         (Marzo ? 1 : 0) + (Abril ? 1 : 0) + 
                         (Mayo ? 1 : 0) + (Junio ? 1 : 0) + 
                         (Julio ? 1 : 0) + (Agosto ? 1 : 0) +
                         (Septiembre ? 1 : 0) + (Octubre ? 1 : 0) + 
                         (Noviembre ? 1 : 0) + (Diciembre ? 1 : 0);
    }
}