using Company.MVC.Demo.Models;
using Company.MVC.Demo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace Company.MVC.Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISingletonService _singleton01;
        private readonly ISingletonService _singleton02;
        private readonly ITransientService _transient01;
        private readonly ITransientService _transient02;
        private readonly IScopedService _scope02;
        private readonly IScopedService _scope01;

        public HomeController(
            ILogger<HomeController> logger,
            IScopedService Scope01,
            IScopedService Scope02,
            ISingletonService Singleton01,
            ISingletonService Singleton02,
            ITransientService Transient01,
            ITransientService Transient02
            )
        {
            _logger = logger;
            _singleton01 = Singleton01;
            _singleton02 = Singleton02;
            _transient01 = Transient01;
            _transient02 = Transient02;
            _scope02 = Scope02;
            _scope01 = Scope01;
        }

        // /Home/TestLifetime
        public string TestLifetime()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Scope01: {_scope01.GetGuid()}\n");
            stringBuilder.Append($"Scope02: {_scope02.GetGuid()}\n\n");
            stringBuilder.Append($"Transient01: {_transient01.GetGuid()}\n");
            stringBuilder.Append($"Transient02: {_transient02.GetGuid()}\n\n");
            stringBuilder.Append($"Singleton01: {_singleton01.GetGuid()}\n");
            stringBuilder.Append($"Singleton02: {_singleton02.GetGuid()}\n\n");

            return stringBuilder.ToString();
        }

        public IActionResult Index()
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
