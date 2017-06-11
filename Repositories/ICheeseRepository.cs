using System;
using System.Collections.Generic;
using cheeseIt.Models;

namespace cheeseIt.Repositories
{
    public interface ICheeseRepository
    {
        void InsertCheeses(IEnumerable<Cheese> newCheeses);
        IEnumerable<Cheese> GetAll();
    }
}
