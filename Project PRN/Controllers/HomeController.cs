using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project_PRN.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace Project_PRN.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
