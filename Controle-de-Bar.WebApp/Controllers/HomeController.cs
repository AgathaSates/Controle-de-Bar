using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Bar.WebApp.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
