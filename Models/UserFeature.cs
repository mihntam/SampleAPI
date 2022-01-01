using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Models
{
    public class UserModel
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
        
}
