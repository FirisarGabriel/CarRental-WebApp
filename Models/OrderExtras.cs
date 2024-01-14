using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proiect_Rent_A_Car.Models
{
    public class OrderExtras
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ExtrasId { get; set; }
        public ICollection<Order> Order { get; set; }
        public ICollection<Extras> Extras { get; set; }
    }
}