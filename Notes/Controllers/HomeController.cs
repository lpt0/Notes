/** HomeController.cs
 * 
 * TODO
 * 
 * Author: Haran
 * Date: December 6th, 2020
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// The index route handler.
        /// </summary>
        /// <returns>The `index` page view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// The about page route handler.
        /// </summary>
        /// <returns>The `about` page view.</returns>
        public IActionResult About()
        {
            return View();
        }

        /// <summary>
        /// The default error page handler.
        /// </summary>
        /// <returns>The error page view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
