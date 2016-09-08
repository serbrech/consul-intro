using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        public HomeController ()
        {
          
        }

        public IActionResult Index()
        {
            return Content("<h1>Hello from controller</h1>","text/html");
        }
    }
}
