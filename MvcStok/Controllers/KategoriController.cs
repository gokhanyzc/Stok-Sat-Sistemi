using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity; //mvcstok projesi içinde models klasörü içinde bulunan entity klasörünü çağırdık.
using PagedList;  //paging için gerekli library.
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities(); //db:nesne mvcdbstokentities:modeli tutuyor modelde tablolar var.

        public ActionResult Index(int sayfa = 1)   //? ilgili değerin null değeri almasını sağlıyor.
        {
            //var degerler = db.TBL_KATEGORILER.ToList(); //Kısa yoldan Kategori tablosundaki değerleri listeliyor.

            var degerler = db.TBL_KATEGORILER.ToList().ToPagedList(sayfa, 4); //1.değerden başla 4 tane değer getir anlamında.
            return View(degerler); //geriye degerleri döndürüyor.
        }

        //HttpPost: Sayfaya herhangi bir post işlemi yapıldığı zaman (mesela butona kaydet basıldıgında)
        //sadece sayfayı geri yükle herhangi bir  işlem yapma
        [HttpGet]
        public ActionResult YeniKategori()
        {

            return View();
        }
        [HttpPost]


        //Kategori ekleme.
        public ActionResult YeniKategori(TBL_KATEGORILER p1)
        {
            if (!ModelState.IsValid) //doğrulanma işlemi yapılmadıysa.
            {
                return View("YeniKategori");
            }
            db.TBL_KATEGORILER.Add(p1);
            db.SaveChanges(); //değişiklikleri kaydediyor.
            return View();


        }


        //KATEGORİ SİLME
        public ActionResult KategoriSil(int id)
        {
            var kategori = db.TBL_KATEGORILER.Find(id);
            db.TBL_KATEGORILER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        //SAYFALAR ARASI VERİ TAŞIMA 
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBL_KATEGORILER.Find(id);
            return View("KategoriGetir", ktgr);
        }

        //GÜNCELLEME
        public ActionResult KategoriGuncelle(TBL_KATEGORILER p1)
        {
            var ktg = db.TBL_KATEGORILER.Find(p1.KATEGORIID);
            ktg.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");


        }


    }
}