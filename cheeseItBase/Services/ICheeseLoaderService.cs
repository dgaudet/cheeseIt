using System;
using cheeseIt.Models;
using Microsoft.AspNetCore.Http;

namespace cheeseIt.Services
{
    public interface ICheeseLoaderService
    {
        int LoadCheeses(string fileName, DateTime dateRecieved);
        int LoadCheeses(IFormFile file, DateTime dateRecieved);
    }
}
