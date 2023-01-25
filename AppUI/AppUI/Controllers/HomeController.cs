using App.DataAccess.Contexts;
using AppBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContex _context;
        public HomeController(AppDbContex contex)
        {
            _context = contex;
        }

        public IActionResult Index()
        {
            HomeViewModel homeView = new()
            {
                TeamMembers=_context.TeamMembers
            };
            return View(homeView);
        }


    }
}