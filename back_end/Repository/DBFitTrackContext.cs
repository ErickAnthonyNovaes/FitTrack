using Microsoft.EntityFrameworkCore;
using System;

public class DBFitTrackContext : DbContext
{
    public DBFitTrackContext(DbContextOptions<DBFitTrackContext> options) : base(options) { }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Exercicio> Exercicios { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}
