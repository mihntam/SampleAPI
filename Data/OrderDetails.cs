using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Data
{

    public class OrderDetails
    {
        public Guid OrderID { get; set; }
        public Order Order { get; set; }
        public Guid ProductID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
