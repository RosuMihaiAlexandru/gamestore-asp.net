using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Gamesmarket.Models;
using Gamesmarket.Domain.Entity;
using System.IO.Pipelines;
using Gamesmarket.Domain.Enum;
using GamesMarket.DAL.Interfaces;

namespace Gamesmarket.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
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