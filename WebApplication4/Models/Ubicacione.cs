using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Ubicacione
{
    public int Id { get; set; }

    public int LibroId { get; set; }

    public string? Estante { get; set; }

    public string? Seccion { get; set; }

    public string? CodigoUbicacion { get; set; }

    public virtual Libro Libro { get; set; } = null!;
}
