using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FrontEnd.W.Models
{
    public partial class Gastos
    {
        public int Id { get; set; }
        public double Monto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public string Idcliente { get; set; }
        public int Idcategoria { get; set; }

        public virtual Categorias IdcategoriaNavigation { get; set; }
        public virtual AspNetUsers IdclienteNavigation { get; set; }
    }
}
