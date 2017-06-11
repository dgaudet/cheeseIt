using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using cheeseIt.Models;
using cheeseIt.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cheeseIt.Controllers
{
    public class CheeseController : Controller
    {
        private readonly ICheeseLoaderService _cheeseService;
        private readonly IHostingEnvironment _env;
        
        public CheeseController(ICheeseLoaderService cheeseService, IHostingEnvironment environment){
            _cheeseService = cheeseService;
            _env = environment;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            var dateRecieved = DateTime.Parse("06/13/2017");
            string fileName = _env.ContentRootPath + "/rustydragon_13062017.xml";
            var cheeses = _cheeseService.LoadCheeses(fileName, dateRecieved);

            var futureCheesePrices = new Dictionary<String, List<Decimal?>>();
            int daysToCalculate = 7;
            foreach (var cheese in cheeses)
            {
                var futurePrices = new List<Decimal?>();
                for (int i = 0; i < daysToCalculate; i++)
                {
                    futurePrices.Add(cheese.PriceForDay(dateRecieved.AddDays(i)));
                }
                futureCheesePrices.Add(cheese.Name, futurePrices);
            }

            var viewModel = new CheeseViewModel
            {
                Cheeses = cheeses,
                FutureCheesePrices = futureCheesePrices
            };
            return View(viewModel);
        }
    }
}
