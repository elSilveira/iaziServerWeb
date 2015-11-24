using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IaziServerWeb.Models
{
    [Table("EmpresaClienteServico")]
    public class EmpresaClienteServico
    {
        [Key]
        public int idEmpresaClienteServico { get; set; }
        
        public int idEmpresaCliente { get; set; }
        public virtual EmpresaCliente empresaCliente { get; set; }

        public int idEmpresaServico { get; set; }
        public virtual EmpresaServico empresaServico { get; set; }

    }
}
