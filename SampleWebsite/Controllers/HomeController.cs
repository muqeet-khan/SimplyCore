using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SimplyCore.Foundation.Service;
using SampleWebsite.Models;
using Microsoft.AspNet.Hosting;

namespace SampleWebsite.Controllers
{
    public class HomeController : Controller
    {
        ISimplyRecorder _simply;
        private IHostingEnvironment _env;

        public HomeController(IHostingEnvironment env,ISimplyRecorder simply)
        {
            _simply = simply;
            _env = env;
        }

        public IActionResult Index()
        {
            _simply.AddInfo("Home:Action","resulted in index action");
            return View();
        }

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

        public IActionResult Error()
        {
            return View();
        }
    }
}
