using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace Project_PRN.Controllers
{
    public class AdminArticleController : Controller
    {
        private readonly PRN211_TechnologyNewsContext db;

        public AdminArticleController(PRN211_TechnologyNewsContext db) => this.db = db;

        // GET: AccountController
        public ActionResult Index()
        {
            var model =
                db.Articles
                .Include(obj => obj.Category)
                .Include(obj => obj.CreatedAccountUsernameNavigation);

            return View(model.ToList());
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int? id)
        {
            //invalid parameter
            if (id == null)
            {
                return NotFound();
            }

            //don't exist any product has this id
            var account = db.Articles.FirstOrDefault(p => p.Id.Equals(id));
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
        public ActionResult Create(Article article)
        {
            //valid data form
            if (ModelState.IsValid)
            {
                db.Add(article);//add cache
                db.SaveChanges();//add db
                return RedirectToAction(nameof(Index));
            }
            //keep state.
            return View(article);
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int? id)
        {
            //invalid parameter
            if (id == null)
            {
                return NotFound();
            }

            //don't exist any product has this id
            var article = db.Articles.ToList().Find(obj => obj.Id.Equals(id));
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Article article)
        {
            //invalid parameter
            if (!id.Equals(article.Id))
            {
                return NotFound();
            }

            //valid data from client.
            if (ModelState.IsValid)
            {
                db.Update(article);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int? id)
        {
            var article = db.Articles.ToList().Find(obj => obj.Id.Equals(id));
            db.Articles.Remove(article);
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
