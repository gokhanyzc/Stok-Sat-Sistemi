using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity; //lib

namespace MvcStok.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis

        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {

            return View();
        }
        [HttpPost]

        public ActionResult YeniSatis(TBL_SATISLAR p1)
        {

            db.TBL_SATISLAR.Add(p1);
            db.SaveChanges();

            return View("Index");
        }
    }
}