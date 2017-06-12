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
        private readonly ICheeseService _cheeseService;
        
        public CheeseController(ICheeseService cheeseService){
            _cheeseService = cheeseService;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            var cheeses = _cheeseService.GetAllCheeses().ToArray();
            var futureCheesePrices = _cheeseService.FutureCheesePrices(7);

            var message = "";
            if (cheeses.Length == 0)
            {
                message = "There are no cheeses loaded yet, please load some cheeses";
            }

            var viewModel = new CheeseViewModel
            {
                Cheeses = cheeses,
                FutureCheesePrices = futureCheesePrices,
                Message = message
            };
            return View(viewModel);
        }
    }
}
