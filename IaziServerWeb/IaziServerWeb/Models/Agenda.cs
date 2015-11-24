using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IaziServerWeb.Models
{
    [Table("Agenda")]
    public class Agenda
    {
        [Key]
        public int idAgenda { get; set; }

        public DateTime horarioAgenda { get; set; }
        
        public int idEmpresaCliente { get; set; }
        public virtual EmpresaCliente empresaCliente { get; set; }

        //Horário 0 = Inalterado, 1 = Disponível, 2 = Indisponível
        public int infoHorario { get; set; }

        
    }
}
