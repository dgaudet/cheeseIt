using System;
using System.Collections.Generic;
using cheeseIt.Models;

namespace cheeseIt.Models
{
    public class CheeseViewModel
    {
        public IEnumerable<Cheese> Cheeses {get; set;}
        public Dictionary<String, List<Decimal?>> FutureCheesePrices { get; set; }
        public String Message { get; set; }
    }
}
