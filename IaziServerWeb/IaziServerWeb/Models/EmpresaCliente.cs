using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IaziServerWeb.Models
{
    [Table("EmpresaCliente")]
    public class EmpresaCliente
    {
        [Key]
        public int idEmpresaCliente { get; set; }

        public virtual Empresa empresa { get; set; }
        
        public virtual Cliente cliente { get; set; }

        public int tipoCliente { get; set; } // 0-Cliente 1-funcionario 2-gerente 3-administrador

        public string especializacaoCliente { get; set; }

        public string especializacao2Cliente { get; set; }

        public string especializacao3Cliente { get; set; }
        
    }
}
