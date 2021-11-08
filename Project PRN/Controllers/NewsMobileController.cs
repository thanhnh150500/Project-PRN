using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_PRN.Controllers
{
    public class NewsMobileController : Controller
    {
        public ActionResult MobileNews()
        {
            return View();
        }

        public ActionResult DetailNewsMoblie()
        {
            return View();
        }
    }
}
