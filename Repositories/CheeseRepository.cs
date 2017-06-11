using System;
using System.Collections.Generic;
using cheeseIt.Models;

namespace cheeseIt.Repositories
{
    public class CheeseRepository : ICheeseRepository
    {
        private List<Cheese> _cheeseData;

        public CheeseRepository(){
            _cheeseData = new List<Cheese>();
        }

        public void InsertCheeses(IEnumerable<Cheese> newCheeses){
            _cheeseData.Clear();
            foreach (var cheese in newCheeses)
            {
                _cheeseData.Add(cheese);
            }
        }

        public IEnumerable<Cheese> GetAll(){
            return _cheeseData;
        }
    }
}
