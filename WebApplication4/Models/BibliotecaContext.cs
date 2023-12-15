using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Models;

public partial class BibliotecaContext : DbContext
{
    public BibliotecaContext()
    {
    }

    public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Editoriale> Editoriales { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Resena> Resenas { get; set; }

    public virtual DbSet<Ubicacione> Ubicaciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Juan\\SQLEXPRESS; Database=Biblioteca; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autore>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Biografia)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Editoriale>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AutorId).HasColumnName("AutorID");
            entity.Property(e => e.EditorialId).HasColumnName("EditorialID");
            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Autor).WithMany(p => p.Libros)
                .HasForeignKey(d => d.AutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Libros_Autores");

            entity.HasOne(d => d.Editorial).WithMany(p => p.Libros)
                .HasForeignKey(d => d.EditorialId)
                .HasConstraintName("FK_Libros_Editoriales");

            entity.HasMany(d => d.Categoria).WithMany(p => p.Libros)
                .UsingEntity<Dictionary<string, object>>(
                    "LibrosCategoria",
                    r => r.HasOne<Categoria>().WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LibrosCategorias_Categorias"),
                    l => l.HasOne<Libro>().WithMany()
                        .HasForeignKey("LibroId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LibrosCategorias_Libros"),
                    j =>
                    {
                        j.HasKey("LibroId", "CategoriaId");
                        j.ToTable("LibrosCategorias");
                        j.IndexerProperty<int>("LibroId").HasColumnName("LibroID");
                        j.IndexerProperty<int>("CategoriaId").HasColumnName("CategoriaID");
                    });
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EstadoPrestamo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaDevolucion).HasColumnType("date");
            entity.Property(e => e.FechaPrestamo).HasColumnType("date");
            entity.Property(e => e.LibroId).HasColumnName("LibroID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Libro).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.LibroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prestamos_Libros");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prestamos_Usuarios");
        });

        modelBuilder.Entity<Resena>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Comentario)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaResena).HasColumnType("date");
            entity.Property(e => e.LibroId).HasColumnName("LibroID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Libro).WithMany(p => p.Resenas)
                .HasForeignKey(d => d.LibroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Resenas_Libros");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Resenas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Resenas_Usuarios");
        });

        modelBuilder.Entity<Ubicacione>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CodigoUbicacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estante)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LibroId).HasColumnName("LibroID");
            entity.Property(e => e.Seccion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Libro).WithMany(p => p.Ubicaciones)
                .HasForeignKey(d => d.LibroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ubicaciones_Libros");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
