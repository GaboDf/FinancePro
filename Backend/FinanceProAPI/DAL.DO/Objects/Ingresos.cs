using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DO.Objects
{
    public class Ingresos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public string Idusuario { get; set; }
        public int Idcategoria { get; set; }

        public virtual Categorias Categoria { get; set; }
    }
}
