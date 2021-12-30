using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Data
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

    }
}
