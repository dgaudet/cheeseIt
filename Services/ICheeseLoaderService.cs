using System;
using cheeseIt.Models;
using Microsoft.AspNetCore.Http;

namespace cheeseIt.Services
{
    public interface ICheeseLoaderService
    {
        Cheese[] LoadCheeses(string fileName, DateTime dateRecieved);
        Cheese[] LoadCheeses(IFormFile file, DateTime dateRecieved);
    }
}
