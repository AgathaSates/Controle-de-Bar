using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Bar.WebApp.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.BannerUrl = Url.Content("~/imagens/banner-1.png");
        return View();
    }
}
