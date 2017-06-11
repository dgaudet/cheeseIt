using System;
namespace cheeseIt.Models
{       
    public static class CheeseConstants {
		public static readonly Decimal MAX_PRICE = 20M;
		public static readonly Decimal STANARD_PRICE_DECREASE_RATE = 0.05M;
    }

    public class Cheese
    {
        public string Name { get; set; }
        public DateTime DateRecieved { get; set; }
        public DateTime? BestBeforeDate { get; set; }
        public int? DaysToSell { get; set; }
        public Decimal? Price { get; set; }
        public CheeseType Type { get; set; }

        public Decimal? PriceForDay(DateTime day) {
            if (Price == null || DateRecieved == DateTime.MinValue)
            {
                return null;
            }
            var daysOld = Convert.ToDecimal((day.Date - DateRecieved.Date).TotalDays);

            var decreaseRate = CheeseConstants.STANARD_PRICE_DECREASE_RATE;
            if (BestBeforeDate != null)
            {
                DateTime nonNullBestBefore = BestBeforeDate ?? DateTime.Now;
                if (nonNullBestBefore.Date < day.Date)
                {
                    decreaseRate = decreaseRate * 2;
                }
            }
            var calculatedPrice = Price - Price * decreaseRate * daysOld;
			if (calculatedPrice > CheeseConstants.MAX_PRICE)
			{
				return CheeseConstants.MAX_PRICE;
			}
            if (calculatedPrice < 0)
            {
                return 0M;
            }
            return calculatedPrice;
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
