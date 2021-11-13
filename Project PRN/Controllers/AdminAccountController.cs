﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_PRN.Models;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Session;
using Project_PRN.Controllers;
using Project_PRN.ExceptionHandler;
using Project_PRN.Authorization;

namespace Project_PRN.Controllers.Admin
{
    public class AdminAccountController : Controller
    {

        private readonly PRN211_TechnologyNewsContext db;

        public AdminAccountController(PRN211_TechnologyNewsContext db) => this.db = db;

        // GET: AccountController
        public ActionResult Index()
        {
            var account = HttpContext.Session.GetObjectFromJson<Account>("account");

            //Get current controller's name context.
            //ref: https://stackoverflow.com/questions/18248547/get-controller-and-action-name-from-within-controller
            var controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            //Get current action's name context.
            var actionName = ControllerContext.RouteData.Values["action"].ToString();

            //Can access server resource(controllerName, actionName) by account
            if (account != null && AuthorizationHelper.IsValidAccess(account, controllerName, actionName))
            {
                var model = db.Accounts.Include(obj => obj.Role).ToList();
                return View(model);
            }
            //Can't access server resource by account OR Don't login.
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
