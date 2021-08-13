using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FrontEnd.W.Models
{
    public partial class Categorias
    {
        public Categorias()
        {
            Gastos = new HashSet<Gastos>();
            Ingresos = new HashSet<Ingresos>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Gastos> Gastos { get; set; }
        public virtual ICollection<Ingresos> Ingresos { get; set; }
    }
}
