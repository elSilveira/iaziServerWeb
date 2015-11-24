using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IaziServerWeb.Models
{
    [Table("EmpresaCliServ")]
    public class EmpresaCliServ
    {
        [Key]
        public int idEmpresaCliServ { get; set; }
        
        public virtual EmpresaCliente empresaCliente { get; set; }

        public virtual EmpresaServico empresaServico { get; set; }

    }
}
