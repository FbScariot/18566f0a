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
    [Authorize]
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Menus
        public ActionResult Index()
        {
            var menus = db.Menus.Include(m => m.MenuPai).Include(m => m.AspNetUser);

            if (!User.IsInRole("Administrador"))
            {
                menus = menus.Where(m => m.AspNetUser.Email == User.Identity.Name);
            }

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
            ViewBag.IdAspNetUsers = new SelectList(db.Users, "Id", "UserName");
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

                ViewBag.IdAspNetUsers = new SelectList(db.Users, "Id", "UserName", menu.AspNetUser.Id);
                ViewBag.CodMenuPai = new SelectList(db.Menus, "Codigo", "Titulo", menu.MenuPai.Codigo);
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

            List<Menu> menus = null;
            List<ApplicationUser> usuarios = null;

            if (User.IsInRole("Administrador"))
            {
                menus = db.Menus.ToList();
                usuarios = db.Users.ToList();
            }
            else
            {
                menus = db.Menus.Where(m => m.AspNetUser.Email == User.Identity.Name).ToList();
                usuarios = db.Users.Where(u => u.Email == User.Identity.Name).ToList();
            }

            ViewBag.IdAspNetUsers = new SelectList(usuarios, "Id", "UserName", menu.AspNetUser.Id);
            ViewBag.CodMenuPai = new SelectList(menus, "Codigo", "Titulo", menu.MenuPai.Codigo);
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
            ViewBag.IdAspNetUsers = new SelectList(db.Users, "Id", "UserName", menu.AspNetUser.Id);
            ViewBag.CodMenuPai = new SelectList(db.Menus, "Codigo", "Titulo", menu.MenuPai.Codigo);
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
