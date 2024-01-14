using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect_Rent_A_Car.Models
{
    public class Listing
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public virtual Cars Car { get; set; }
        public int Price { get; set; }
        public int WarrantyCost { get; set; }
        public int Description { get; set; }
        [NotMapped]
        public bool IsAdmin { get; set; }
    }
}