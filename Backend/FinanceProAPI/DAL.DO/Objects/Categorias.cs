using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DO.Objects
{
    public  class Categorias
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
