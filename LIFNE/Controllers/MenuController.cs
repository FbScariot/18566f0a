using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LIFNE.Models;

namespace LIFNE.Controllers
{
    public class MenuController : Controller
    {
        private LIFNEEntities1 db = new LIFNEEntities1();

        // GET: Menus
        public ActionResult Index()
        {
            var menus = db.Menus.Include(m => m.AspNetUser).Include(m => m.Menu2);
            return View(menus.ToList());
        }

        // GET: Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Menus/Create
        public ActionResult Create()
        {
            ViewBag.IdAspNetUsers = new SelectList(db.AspNetUsers, "Id", "UserName");
            ViewBag.CodMenuPai = new SelectList(db.Menus, "Codigo", "Titulo");
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Tipo,Titulo,Conteudo,CodMenuPai,IdAspNetUsers")] Menu menu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Menus.Add(menu);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdAspNetUsers = new SelectList(db.AspNetUsers, "Id", "UserName", menu.IdAspNetUsers);
                ViewBag.CodMenuPai = new SelectList(db.Menus, "Codigo", "Titulo", menu.CodMenuPai);
                return View(menu);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAspNetUsers = new SelectList(db.AspNetUsers, "Id", "UserName", menu.IdAspNetUsers);
            ViewBag.CodMenuPai = new SelectList(db.Menus, "Codigo", "Titulo", menu.CodMenuPai);
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Tipo,Titulo,Conteudo,CodMenuPai,IdAspNetUsers")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAspNetUsers = new SelectList(db.AspNetUsers, "Id", "UserName", menu.IdAspNetUsers);
            ViewBag.CodMenuPai = new SelectList(db.Menus, "Codigo", "Titulo", menu.CodMenuPai);
            return View(menu);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
