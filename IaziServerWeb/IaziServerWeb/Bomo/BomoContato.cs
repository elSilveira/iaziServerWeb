using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IaziServerWeb.Bomo
{
    [Table("BomoContato")]
    public class BomoContato
    {
        [Key]
        public int idBomoContato { get; set; }
        public string email { get; set; }
        public string nome { get; set; }
        public string assunto { get; set; }
        public string mensagem { get; set; }
    }
}