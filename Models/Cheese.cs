using System;
namespace cheeseIt.Models
{
    public class Cheese
    {
        public string Name { get; set; }
        public DateTime? BestBeforeDate { get; set; }
        public int? DaysToSell { get; set; }
        public Decimal? Price { get; set; }
        public string Type { get; set; }
    }

    public class Containers {
        public Cheese[] Cheeses { get; set; }
    }
}
