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
                try{
					if (file.Length > 0)
					{
						var dateRecieved = DateTime.Parse("06/13/2017");
						var numCheesesLoaded = _cheeseService.LoadCheeses(file, dateRecieved);

						ViewBag.result = $"Successfully loaded {numCheesesLoaded} Cheeses.";
						ViewBag.resultCssClass = "alert alert-success";
                        ViewBag.showLink = true;
					}
					else
					{
						ViewBag.result = $"No Cheeses loaded.";
						ViewBag.resultCssClass = "alert alert-warning";
                        ViewBag.showLink = false;
					}
                } catch (Exception){
					ViewBag.result = $"There was a problem with the your Cheese file. Please try a different file.";
					ViewBag.resultCssClass = "alert alert-danger";
                    ViewBag.showLink = false;
                }

				return View();
			}
			return View(model);
		}
    }
}
