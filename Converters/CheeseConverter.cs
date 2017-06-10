using System;
using cheeseIt.Models;

namespace cheeseIt.Converters
{
    public class CheeseConverter
    {
        public Cheese CheeseFromItem(Item item){
            if (string.IsNullOrEmpty(item.Name))
            {
                return null;
            } else {
                int? daysToSell = null;
                if (!string.IsNullOrEmpty(item.DaysToSell))
                {
                    int daysToSellNotNull;
                    if (int.TryParse(item.DaysToSell, out daysToSellNotNull))
                    {
                        daysToSell = daysToSellNotNull;
                    }
                }

                decimal? price = null;
                if (!string.IsNullOrEmpty(item.Price))
				{
                    decimal priceNotNull;
                    if (decimal.TryParse(item.Price, out priceNotNull))
					{
						price = priceNotNull;
					}
				}

                DateTime? bestBeforeDate = null;
                if (!string.IsNullOrEmpty(item.BestBeforeDate))
				{
					DateTime bestBeforeNotNull;
                    if (DateTime.TryParse(item.BestBeforeDate, out bestBeforeNotNull))
					{
						bestBeforeDate = bestBeforeNotNull;
					}
				}

                return new Cheese
                {
                    Name = item.Name,
                    BestBeforeDate = bestBeforeDate,
                    DaysToSell = daysToSell,
                    Price = price,
                    Type = item.Type
                };
            }
        }
    }
}