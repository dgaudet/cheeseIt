using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cheeseIt.Models;
using cheeseIt.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cheeseIt.Controllers
{
    public class CheeseController : Controller
    {
        private readonly CheeseLoaderService _cheeseService;
        
        public CheeseController(CheeseLoaderService cheeseService){
            _cheeseService = cheeseService;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            var dateRecieved = DateTime.Parse("06/13/2017");
            var cheeses = _cheeseService.LoadCheeses(dateRecieved);
            return View(cheeses);
        }
    }
}
