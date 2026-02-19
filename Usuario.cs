using System.ComponentModel.DataAnnotations.Schema;

public class Usuario
{
    public int Id { get; set; }

    [Column("Usuario")]
    public string NombreUsuario { get; set; }
    
    public string Contrasena { get; set; }
}

public class LoginRequest
{
    public string Usuario { get; set; }
    public string Contrasena { get; set; }
}