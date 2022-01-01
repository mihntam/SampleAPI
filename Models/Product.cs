using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Guid? CategoryId { get; set; }
        
    }

    public class ProductModel : Product
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
    }
}
