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
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Principal(int? id)
        {
            if (id != null)
            {
                Menu menu = db.Menus.Find(id);
                ViewBag.IFrameSrc = menu.Conteudo;
            }

            var menus = db.Menus.Include(m => m.AspNetUser).Include(m => m.MenuPai);

            return View(menus.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}