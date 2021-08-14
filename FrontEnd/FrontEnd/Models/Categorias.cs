using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class Categorias
    {
        public Categorias()
        {
            // Gastos = new HashSet<Gastos>();
            //Ingresos = new HashSet<Ingresos>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
