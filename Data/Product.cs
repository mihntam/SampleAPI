using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Data
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; }

        [Required]
        [MaxLength(250)]
        public string ProductName { get; set; }

        public string Image { get; set; }
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public Guid? CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
        public Product()
        {
            OrderDetails = new List<OrderDetails>();
        }
    }
}
