using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proiect_Rent_A_Car.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string AgentId { get; set; }
        public virtual ApplicationUser Agent { get; set; }
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public ICollection<OrderExtras> OrderExtras { get; set; }
        public ICollection<Extras> Extras { get; set; }
        public int TotalPrice { get; set; }
    }
}