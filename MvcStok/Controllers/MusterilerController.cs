using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity; // LİB
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class MusterilerController : Controller
    {
        // GET: Musteriler
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p, int sayfa = 1)
        {
            //var degerler = db.TBL_MUSTERILER.ToList();
           // var degerler = db.TBL_MUSTERILER.ToList().ToPagedList(sayfa, 4);
            var degerler = from d in db.TBL_MUSTERILER select d; //arama paneli için
            if (!string.IsNullOrEmpty(p)) //eğer p boş değilse arama paneli çalışır.
            {
                degerler = degerler.Where(m => m.MUSTERIAD.Contains(p));
            }
            return View(degerler.ToList().ToPagedList(sayfa,4));
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]

        //MÜŞTERİ EKLEME
        public ActionResult YeniMusteri(TBL_MUSTERILER p2)
        {

            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TBL_MUSTERILER.Add(p2);
            db.SaveChanges();
            return View();

        }
        //MÜŞTERİ SİLME
        public ActionResult MusteriSil(int id)
        {
            var musteri = db.TBL_MUSTERILER.Find(id);
            db.TBL_MUSTERILER.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //SAYFALAR ARASI VERİ TAŞIMA
        public ActionResult MusteriGetir(int id)
        {
            var musterii = db.TBL_MUSTERILER.Find(id);
            return View("MusteriGetir", musterii);

        }

        //GÜNCELLEME
        public ActionResult MusteriGuncelle(TBL_MUSTERILER p1)
        {
            var mstri = db.TBL_MUSTERILER.Find(p1.MUSTERIID);
            mstri.MUSTERIAD = p1.MUSTERIAD;
            mstri.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}