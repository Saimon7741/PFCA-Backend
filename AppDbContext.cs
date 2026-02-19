using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Artesano> Artesanos { get; set; }
    public DbSet<ArtesanoSiti> ArtesanosSiti { get; set; }
    public DbSet<ArtesanoCN> ArtesanosCN { get; set; }
    public DbSet<ArtesanoRP> ArtesanosRP { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artesano>()
            .Property(a => a.TotalParticipaciones)
            .HasDefaultValue(0);
            
        modelBuilder.Entity<ArtesanoSiti>()
            .Property(a => a.TotalParticipaciones)
            .HasDefaultValue(0);
            
        modelBuilder.Entity<ArtesanoCN>()
            .Property(a => a.Total)
            .HasDefaultValue(0);
    }
}