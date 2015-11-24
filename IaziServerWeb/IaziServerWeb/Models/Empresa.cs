using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IaziServerWeb.Models
{
    [Table("Empresas")]
    public class Empresa
    {

        [Key]
        public int idEmpresa { get; set; }
        
        public string nomeEmpresa { get; set; }
        
        public string cidadeEmpresa { get; set; }
        
        public string estadoEmpresa { get; set; }
        
        public string ruaEmpresa { get; set; }

        public string bairroEmpresa { get; set; }

        public string geoLatitudeEmpresa { get; set; }

        public string geoLongitudeEmpresa { get; set; }

        public string cepEmpresa { get; set; }

        public int numeroEmpresa { get; set; }

        public string imagemEmpresa { get; set; }

        public string infoEmpresa { get; set; }

        [NotMapped]
        public string tipoServico { get; set; }
    }
}
