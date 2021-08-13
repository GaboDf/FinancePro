using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataModels
{
    public class Gastos
    {
        public int Id { get; set; }
        public double Monto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public string Idcliente { get; set; }
        public int Idcategoria { get; set; }

        public Categorias Categoria { get; set; }
    }
}
