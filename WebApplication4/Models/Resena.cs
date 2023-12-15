using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Resena
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int LibroId { get; set; }

    public int? Puntuacion { get; set; }

    public string? Comentario { get; set; }

    public DateTime? FechaResena { get; set; }

    public virtual Libro Libro { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
