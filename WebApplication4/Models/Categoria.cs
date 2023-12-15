using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Categoria
{
    public int Id { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
