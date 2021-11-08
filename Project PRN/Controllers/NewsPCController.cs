using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_PRN.Controllers
{
    public class NewsPCController : Controller
    {
        public ActionResult PCnews()
        {
            return View();
        }

        public ActionResult DetailNewPC()
        {
            return View();
        }
    }
}
