using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IaziServerWeb.Bomo
{
    [Table("BomoLancamentos")]
    public class BomoLancamentos
    {
        [Key]
        public int idBomoLancamento { get; set; }
        public BomoEmpresa BomoEmpresa { get; set; }
        public BomoCliente BomoCliente { get; set; }
        public double valorVenda { get; set; }
        public DateTime dataVenda { get; set; }

    }
}