using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Data
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string OrderStatus { get; set; }
        public string Receiver { get; set; }
        public string Adresss { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }
    }
}
