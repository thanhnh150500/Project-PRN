using Microsoft.AspNetCore.Mvc;
using Project_PRN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_PRN.Controllers
{
    public class NewsMobileController : Microsoft.AspNetCore.Mvc.Controller
    {
        PRN211_TechnologyNewsContext context = new PRN211_TechnologyNewsContext();
        public ActionResult MobileNews()
        {
            var articles = context.Articles.ToList();
            var query = (from items in articles
                         where items.CategoryId == 1
                         select items).ToList();
            ViewBag.Articles = query;
            return View();
        }

        public ActionResult DetailNewsMoblie(int id)
        {
            var articles = context.Articles.ToList();
            var query = (from items in articles
                         where items.Id == id
                         select items).ToList();
            ViewBag.Articles = query;
            return View();
        }
    }
}
