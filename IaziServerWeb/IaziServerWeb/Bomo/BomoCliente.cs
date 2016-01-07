using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IaziServerWeb.Bomo
{
    [Table("BomoCliente")]
    public class BomoCliente
    {
        [Key]
        public int idBomoCliente { get; set; }

        public string nome { get; set; }

        public string sobrenome { get; set; }

        [Index(IsUnique = true)]
        public string email { get; set; }

        public string cnh { get; set; }
    }
}