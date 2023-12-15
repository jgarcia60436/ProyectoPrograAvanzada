using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
}
