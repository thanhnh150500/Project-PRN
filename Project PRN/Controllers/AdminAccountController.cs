using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_PRN.Models;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using Microsoft.EntityFrameworkCore;

namespace Project_PRN.Controllers.Admin
{
    public class AdminAccountController : Controller
    {

        private readonly PRN211_TechnologyNewsContext db;

        public AdminAccountController(PRN211_TechnologyNewsContext db) => this.db = db;

        // GET: AccountController
        public ActionResult Index()
        {
            var model = db.Accounts.Include(obj => obj.Role).ToList();
            return View(model);
        }

        // GET: AccountController/Details/5
        public ActionResult Details(string username)
        {
            //invalid parameter
            if (username == null)
            {
                return NotFound();
            }

            //don't exist any product has this id
            var account = db.Accounts.FirstOrDefault(p => p.Username.Equals(username));
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Account account)
        {
            //valid data form
            if (ModelState.IsValid)
            {
                db.Add(account);//add cache
                db.SaveChanges();//add db
                return RedirectToAction(nameof(Index));
            }
            //keep state.
            return View(account);
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(string username)
        {
            //invalid parameter
            if (username == null)
            {
                return NotFound();
            }

            //don't exist any product has this id
            var account = db.Accounts.ToList().Find(obj => obj.Username.Equals(username));
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int username, Account account)
        {
            //invalid parameter
            if (!username.Equals(account.Username))
            {
                return NotFound();
            }

            //valid data from client.
            if (ModelState.IsValid)
            {
                db.Update(account);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(string username)
        {
            var account = db.Accounts.ToList().Find(obj => obj.Username.Equals(username));
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
