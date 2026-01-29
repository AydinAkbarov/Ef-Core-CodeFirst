using Microsoft.AspNetCore.Mvc;

namespace SimpleStoreSite.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
