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
        public int idClienteServico { get; set; }

        public int idCliente { get; set; }
        public virtual Cliente cliente { get; set; }

        public int idEmpresaClienteServico { get; set; }
        public virtual EmpresaClienteServico empresaClienteServico{get; set;}

        // 0 - Cliente enviou
        // 1 - Sucesso
        // 2 - Resposta Empresa
        // 3 - Treplica Cliente
        public int statusServico { get; set; }

        public double valorServico { get; set; }

        public Nullable<System.DateTime> dataServico { get; set; }

        public Nullable<System.DateTime> dataAlternativa { get; set; }

        public Nullable<System.DateTime>  dataResposta { get; set; }
    }
}

    