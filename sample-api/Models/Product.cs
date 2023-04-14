using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_api.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
    }
}