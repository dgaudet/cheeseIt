using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cheeseIt.Models;
using cheeseIt.Services;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cheeseIt.Controllers
{
    public class LoadFileController : Controller
    {
        private readonly ICheeseLoaderService _cheeseService;

        public LoadFileController(ICheeseLoaderService cheeseService){
            _cheeseService = cheeseService;
        }

		// GET: /<controller>/
		public IActionResult Load()
        {
            return View();
		}

		// POST: LoadFile/Load
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Load([Bind("FileName,ReceivedDate")] LoadFileViewModel model, IFormFile file)
		{
			if (ModelState.IsValid)
			{
                if (file.Length > 0)
                {
					var dateRecieved = DateTime.Parse("06/13/2017");
                    var cheeses = _cheeseService.LoadCheeses(file, dateRecieved);    
                }

				return RedirectToAction("Load");
			}
			return View(model);
		}
    }
}
