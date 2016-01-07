using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IaziServerWeb.Bomo
{
    [Table("BomoUsuario")]
    public class BomoUsuario
    {
        [Key]
        public int idBomoUsuario { get; set; }
        public string senha { get; set; }

        public virtual BomoCliente bomoCliente { get; set; }

        public virtual BomoEmpresa bomoEmpresa { get; set; }
    }
}