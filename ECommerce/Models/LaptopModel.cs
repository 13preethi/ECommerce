using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class LaptopModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Processor { get; set; }
        public string Operating_System { get; set; }
        public double Price { get; set; }
    }
}