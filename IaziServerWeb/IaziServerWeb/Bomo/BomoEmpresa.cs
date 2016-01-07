using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IaziServerWeb.Bomo
{
    [Table("BomoEmpresa")]
    public class BomoEmpresa
    {
        [Key]
        public int idBomoEmpresa { get; set; }

        public string fantasia { get; set; }
        
        [Index(IsUnique = true)]
        public string email { get; set; }

        public string cnpj { get; set; }

        public string rua { get; set; }

        public string cidade { get; set; }

        public string estado { get; set; }

        public string numero { get; set; }

        public string cep { get; set; }
        
    }
}