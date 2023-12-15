using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Autore
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Nacionalidad { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string? Biografia { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
