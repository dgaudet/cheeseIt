using System;
using System.Collections.Generic;
using cheeseIt.Models;

namespace cheeseIt.Services
{
    public interface ICheeseService
    {
        IEnumerable<Cheese> GetAllCheeses();
        Dictionary<String, List<Decimal?>> FutureCheesePrices(int numberOfFutureDays);
    }
}
