using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Data
{
    public enum OrderStatus
    {
        New = 0,
        Payment = 1,
        Complete = 2,
        Cancel = -1
    }

    public class Order
    {
        public Guid OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
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
