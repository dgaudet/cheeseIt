using System;
namespace cheeseIt.Models
{       
    public class Cheese
    {
        public readonly Decimal MAX_PRICE = 20M;

        public string Name { get; set; }
        public DateTime DateRecieved { get; set; }
        public DateTime? BestBeforeDate { get; set; }
        public int? DaysToSell { get; set; }
        public Decimal? Price { get; set; }
        public CheeseType Type { get; set; }
        public Decimal? CurrentPrice {
            get {
                return MAX_PRICE;
            }
        }
    }

    public enum CheeseType {
        Fresh,
        Unique,
        Special,
        Aged,
        Standard
    }
}
