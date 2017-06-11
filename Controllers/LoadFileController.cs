using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cheeseIt.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cheeseIt.Controllers
{
    public class LoadFileController : Controller
    {
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
				return RedirectToAction("Load");
			}
			return View(model);
		}
    }
}
