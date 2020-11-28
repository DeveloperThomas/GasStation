using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Nazwa roli")]
        public string RoleName { get; set; }
    }
}
