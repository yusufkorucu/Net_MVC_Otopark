using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Net_MVC_Otopark.Models;


namespace Net_MVC_Otopark.Controllers
{
    public class OtoparkController : Controller
    {
        DBOtoparkEntities db = new DBOtoparkEntities();
        // GET: Otopark
        public ActionResult Index()
        {
            var arac = db.Arac.ToList().Where(x=>x.AracAktiflik==true);
            return View(arac);
        }

        public ActionResult TumListe()
        {
            var tumliste = db.Arac.ToList();
            return View(tumliste);
        }

        
        public ActionResult AracGiris()
        {
            return View();
        }

        // POST: Otopark/Create
        [HttpPost]
        public ActionResult AracGiris(FormCollection collection,Arac arac)
        {
            var AracSayisi = db.Arac.Where(x=>x.AracAktiflik==true).Count();
            try
            {
                if (AracSayisi<5)
                {
                    db.Arac.Add(arac);
                    arac.AracAktiflik = true;
                    arac.AracGirisTarihi = DateTime.Now;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                // TODO: Add insert logic here
                ViewBag.HataMesaj = "Otoparkımız Dolmuştur";
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AracCikis(int id)
        {

            var arac = db.Arac.Where(x => x.AracID == id).FirstOrDefault();
            arac.AracAktiflik = false;
            arac.AracCikisTarihi = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


      
    }
}
