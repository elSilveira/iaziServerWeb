using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IaziServerWeb.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int idCategoria { get; set; }
        
        public string nomeCategoria { get; set; }

        public string iconeCategoria { get; set; }

        public string infoCategoria { get; set; }

    }
}
