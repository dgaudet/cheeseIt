using System;
using cheeseIt.Models;

namespace cheeseIt.Services
{
    public interface ICheeseLoaderService
    {
        Cheese[] LoadCheeses(string fileName, DateTime dateRecieved);
    }
}
