using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_PRN.Models;
using Project_PRN.Session;
using System.Linq;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace Project_PRN.Controllers
{
    public class AccountController : Controller
    {
        PRN211_TechnologyNewsContext db = new PRN211_TechnologyNewsContext();

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(Account model)
        {
            var test = db.Accounts.ToList().Find(
                acc
                =>
                    (acc.Username == model.Username
                    && acc.Password == model.Password)
               );

            //Have data in DB.
            if (test != null)
            {
                HttpContext.Session.SetObjectAsJson("account", test);
                //TempData["account"] = model; //Pass model data to home controller action index.
                return RedirectToAction("Index", "Home");
            }
            //Don't have data in DB, then keep state.
            else
            {
                return View(model);
            }
        }

        public IActionResult SignUp()
        {
            return View();
        }
    }
}
