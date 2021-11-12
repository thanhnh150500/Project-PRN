using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_PRN.Authorization;
using Project_PRN.ExceptionHandler;
using Project_PRN.Models;
using Project_PRN.Session;
using System;
using System.Linq;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace Project_PRN.Controllers
{
    public class AccountController : Controller
    {
        private readonly PRN211_TechnologyNewsContext db = new PRN211_TechnologyNewsContext();

        //Request to sign in by URL.
        [HttpGet]
        public IActionResult SignIn()
        {
            var account = HttpContext.Session.GetObjectFromJson<Account>("account");
            //First time login, avoid finite loop access denied.
            if (account == null)
            {
                //pass cookie cast to model to server
                //ref: https://www.youtube.com/watch?v=WCDBRL_l6mo
                string key = "MyCookie";
                var cookieVal = Request.Cookies[key];
                var modelOnlyUsername = new Account { Username = cookieVal };


                return View(modelOnlyUsername);
            }
            else
            {
                return ViewByLoginState(account);
            }
        }

        //Request to sign in by submitting form
        [HttpPost]
        public IActionResult SignIn(Account model)
        {
            var account = db.Accounts.ToList().Find(
            acc
            =>
                (acc.Username == model.Username
                && acc.Password == model.Password)
           );

            //Don't  have data in DB.
            if (account == null)
            {
                ViewBag.NotFoundMsg = "Username or password wrong !!!";
                return View(model);
            }
            //Have data in DB.
            else
            {
                HttpContext.Session.SetObjectAsJson("account", account);

                //create Cookie for login when ss end
                //ref: https://www.youtube.com/watch?v=WCDBRL_l6mo
                string key = "MyCookie";
                string value = model.Username;
                CookieOptions cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                };
                Response.Cookies.Append(key, value, cookieOptions);
                //Response.Cookies.Delete(key);//remove session

                return RedirectToAction("Index", "Home");
            }
        }

        //Request to sign up by URL
        [HttpGet]
        public IActionResult SignUp()
        {
            var account = HttpContext.Session.GetObjectFromJson<Account>("account");
            //First time login, avoid finite loop access denied.
            if (account == null)
            {
                return View();
            }
            else
            {
                return ViewByLoginState(account);
            }
        }

        //Request to sign up by submitting form
        [HttpPost]
        public IActionResult SignUp(Account model)
        {
            //Check all field neeeded annotation for model validation
            if (ModelState.IsValid)
            {
                var account = db.Accounts.ToList().Find(
                acc
                =>
                    acc.Username == model.Username
               );

                //Don't data in DB --> add new.
                if (account == null)
                {
                    model.RoleId = 3;//3 is user, default is user
                    db.Accounts.Add(model);
                    db.SaveChanges();
                    ViewBag.Msg = "Regist account successfully. Please enter password to continue !!!";
                    return SignIn(model); //return view login with model registed.
                }
                //Have data in DB.
                else
                {
                    ViewBag.DuplicateMsg = "This username has been already used. Please try another.";
                }
            }

            //Have data in DB, then keep state.
            //OR
            //Model validation don't passed.
            return View(model);
        }

        //Request to sign out by URL
        [HttpGet]
        public IActionResult SignOut()
        {
            //Get current account
            HttpContext.Session.Remove("account");
            return RedirectToAction("Index", "Home");
        }

        //Return suitable view by login state.
        private IActionResult ViewByLoginState(Account account)
        {
            Guard.ThrowIfNull(account, "Account can't be null at AccountController/ViewByLoginState");

            //Get current controller's name context.
            //ref: https://stackoverflow.com/questions/18248547/get-controller-and-action-name-from-within-controller
            var controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            //Get current action's name context.
            var actionName = ControllerContext.RouteData.Values["action"].ToString();

            //Can access server resource(controllerName, actionName) by account
            if (AuthorizationHelper.IsValidAccess(account, controllerName, actionName))
            {
                return View();
            }
            //Can't access server resource by account OR Don't login.
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
