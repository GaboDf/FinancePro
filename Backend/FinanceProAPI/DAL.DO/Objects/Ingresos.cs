﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.DO.Objects
{
    public class Ingresos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public string Idusuario { get; set; }
        public int Idcategoria { get; set; }
        public double Monto { get; set; }

        public virtual Categorias Categoria { get; set; }
    }
}
