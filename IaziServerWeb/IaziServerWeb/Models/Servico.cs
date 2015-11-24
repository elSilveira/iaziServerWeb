using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IaziServerWeb.Models
{
    [Table("Servicos")]
    public class Servico
    {
        [Key]
        public int idServico { get; set; }
        
        public string nomeServico { get; set; }
        
        public virtual Categoria categoria { get; set; }

        public string tipoServico { get; set; }


    }
}
