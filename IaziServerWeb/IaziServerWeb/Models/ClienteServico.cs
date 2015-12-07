using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IaziServerWeb.Models
{
    [Table("ClienteServico")]
    public class ClienteServico
    {
        [Key]
        public int idCliServ { get; set; }

        public virtual Cliente cliente { get; set; }

        public virtual EmpresaCliServ empresaCliServ{get; set;}

        // 0 - Cliente enviou
        // 1 - Sucesso
        // 2 - Resposta Empresa
        // 3 - Treplica Cliente
        public int statusServico { get; set; }

        public double valorServico { get; set; }

        public DateTime dataServico { get; set; }

        public Nullable<DateTime> dataAlternativa { get; set; }

        public Nullable<DateTime>  dataResposta { get; set; }
    }
}

    