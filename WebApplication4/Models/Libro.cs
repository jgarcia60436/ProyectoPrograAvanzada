using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Libro
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public int AutorId { get; set; }

    public string? Genero { get; set; }

    public int? AnioPublicacion { get; set; }

    public string? Isbn { get; set; }

    public int? EditorialId { get; set; }

    public virtual Autore Autor { get; set; } = null!;

    public virtual Editoriale? Editorial { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();

    public virtual ICollection<Ubicacione> Ubicaciones { get; set; } = new List<Ubicacione>();

    public virtual ICollection<Categoria> Categoria { get; set; } = new List<Categoria>();
}
