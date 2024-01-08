using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCaller
{
    public class Users : ResponseBase
    {
        [Key]
        public int? idUser { get; set; }
        
        public string? cUser { get; set; }

        public string? cPass { get; set; }

        public string? cEmail { get; set;}

        public int? nAdministrator { get; set; }

        public int? nManager { get; set; }

        public int? idNegocio { get; set;}

        public int? nValidated { get; set; }

    }
}
