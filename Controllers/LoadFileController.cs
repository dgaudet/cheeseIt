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
		public IActionResult Load([Bind("FileName,ReceivedDate")] LoadFileViewModel model)
		{
			if (ModelState.IsValid)
			{
                var fileName = "/Users/dean/Documents/Code/CodeChallenges/PrintAudit/cheeseIt/rustydragon_13062017.xml";
				var dateRecieved = DateTime.Parse("06/13/2017");
				//string fileName = _env.ContentRootPath + "/rustydragon_13062017.xml";
				var cheeses = _cheeseService.LoadCheeses(fileName, dateRecieved);

				return RedirectToAction("Load");
			}
			return View(model);
		}
    }
}
