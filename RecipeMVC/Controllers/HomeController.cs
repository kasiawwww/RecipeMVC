using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeMVC.Models;
using Domain.Repositories;
using Domain.Models.DTO;

namespace RecipeMVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
           // var repo = new RecipesRepo();
            //var item = new RecipesDTO
            //{
            //    ID = 0,
            //    Body = "coś",
            //    Name = "nazwa",
            //    Category = null,
            //    CategoryId = null,
            //    Image = null
            //};

            //await repo.Add(item);
           // var x = await repo.Get(5);
            return View();
        }
        [Route("oAplikacji")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
