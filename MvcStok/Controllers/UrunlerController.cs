using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity; //mvcstok projesi içinde models klasörü içinde bulunan entity klasörünü çağırdık.
using PagedList;
using PagedList.Mvc;



namespace MvcStok.Controllers
{
    public class UrunlerController : Controller
    {
        // GET: Urunler

        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index(int sayfa = 1)
        {
            //var degerler = db.TBL_URUNLER.ToList();
            var degerler = db.TBL_URUNLER.ToList().ToPagedList(sayfa, 5);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            //DROPDOWNLİST , LİNQ kullanımı
            //tblkategorilerin listesini çek i değişkenine ata.
            List<SelectListItem> degerler = (from i in db.TBL_KATEGORILER.ToList()
                                             select new SelectListItem
                                             {

                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()

                                             }).ToList();
            //ViewBag, Controller'da oluşturulan bir yapıyı View kısmına taşımak için kullanılır. 
            ViewBag.dgr = degerler;  //controller tarafındaki ifadeyi diğer sayfaya taşır.
            return View();
        }
        [HttpPost]

        public ActionResult YeniUrun(TBL_URUNLER p3)
        {
            //firstordefault :linq içinde bulunur.Liste içerisinde seçtiğimiz ilk değeri getirir
            //amaç burada KATEGORIID yi almak.
            var ktg = db.TBL_KATEGORILER.Where(m => m.KATEGORIID == p3.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
            p3.TBL_KATEGORILER = ktg;

            db.TBL_URUNLER.Add(p3);
            db.SaveChanges();
            //  return View();
            return RedirectToAction("Index"); //kaydetme işlemi gerçekleştikten sonra bizi index sayfasına geri yönlendirir.
        }

        public ActionResult UrunSil(int id)
        {
            var urunler = db.TBL_URUNLER.Find(id);
            db.TBL_URUNLER.Remove(urunler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //SAYFALAR ARASI VERİ TAŞIMA
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBL_URUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBL_KATEGORILER.ToList()
                                             select new SelectListItem
                                             {

                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View("UrunGetir", urun);
        }

        //GÜNCELLE
        public ActionResult UrunGuncelle(TBL_URUNLER p1)
        {

            var urn = db.TBL_URUNLER.Find(p1.URUNID);
            urn.URUNAD = p1.URUNAD;
            urn.MARKA = p1.MARKA;
            // urn.URUNKATEGORI = p1.URUNKATEGORI;
            var ktg = db.TBL_KATEGORILER.Where(m => m.KATEGORIID == p1.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
            urn.URUNKATEGORI = ktg.KATEGORIID;
            urn.FIYAT = p1.FIYAT;
            urn.STOK = p1.STOK;

            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}