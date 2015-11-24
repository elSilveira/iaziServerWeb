using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IaziServerWeb.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        public int idCliente { get; set; }

        public string nomeCliente { get; set; }

        public string sobrenomeCliente { get; set; }


        public string telefoneCliente { get; set; }

        [Index(IsUnique = true)]
        public string emailCliente { get; set; }

        public string cidadeCliente { get; set; }

        public string estadoCliente { get; set; }

        public string geoLatitudeCliente { get; set; }

        public string geoLongitudeCliente { get; set; }

    }
}
