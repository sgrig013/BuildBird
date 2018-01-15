using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArchieBirds.Models;

namespace ArchieBirds.Controllers
{
    public enum Units
    {
        Milliarchieops,
        Metric,
        Imperial,
    }

    public enum FormData
    {
        Unit,
        ReturnPath,
    }
    
    public class ArchiesController : Controller
    {
        private ArchiesDBContext db = new ArchiesDBContext();
        private Units preferredUnit = Units.Milliarchieops;
        private string unitCookieName = "unit";

        // GET/POST: Archies
        public ActionResult Index()
        {
            GetCookie();
            return View(db.Archies.ToList());
        }

        // GET: Archies/Details/5
        public ActionResult Details(int? id)
        {
            GetCookie();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Archie archie = db.Archies.Find(id);
                if (archie == null)
                {
                    return HttpNotFound();
                }
                if (archie.Wingspan == 0)
                {
                    ViewBag.ErrorMessage = "Can't calculate Wingspan. Both wings are missing.";
                }

                archie.SetUnit(preferredUnit);
                return View(archie);
            }
            catch (Exception e)
            {
                return View("Error",new HandleErrorInfo(e,"ArchiesController","Details"));
            }
            
        }

        // GET: Archies/Create
        public ActionResult Create()
        {
            GetCookie();
            return View();
        }

        // POST: Archies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Height,Weight,Length,Girth,Wingspan,SpecimenName,WingWidth,Latitude,Longtitude,Wings,HandThings,Scull,Teeth,Feet,Tail,Spine")] Archie archie)
        {
            GetCookie();
            try
            {
                if (ModelState.IsValid)
                {
                    archie.currentUnit = preferredUnit;
                    archie.SetUnit(Units.Metric);
                    db.Archies.Add(archie);
                    db.SaveChanges();
                    return Redirect("/Archies/Details/" + archie.ID);
                }
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "ArchiesController", "Create"));
            }

            return View(archie);
        }

        // GET: Archies/Edit/5
        public ActionResult Edit(int? id)
        {
            GetCookie();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archie archie = db.Archies.Find(id);
            if (archie == null)
            {
                return HttpNotFound();
            }
            archie.SetUnit(preferredUnit);
            return View(archie);
        }

        // POST: Archies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = @"ID,Height,Weight,Length,Girth,Wingspan,SpecimenName,WingWidth,Latitude,Longtitude,
                                                    Wings,HandThings,Scull,Teeth,Feet,Tail,Spine")] Archie archie)
        {
            GetCookie();
            if (ModelState.IsValid)
            {
                archie.currentUnit = preferredUnit;
                archie.SetUnit(Units.Metric);
                db.Entry(archie).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Archies/Details/" + archie.ID);
            }
            return View(archie);
        }

        // GET: Archies/Delete/5
        public ActionResult Delete(int? id)
        {
            GetCookie();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archie archie = db.Archies.Find(id);
            if (archie == null)
            {
                return HttpNotFound();
            }
            archie.SetUnit(preferredUnit);
            return View(archie);
        }

        // POST: Archies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Archie archie = db.Archies.Find(id);
            db.Archies.Remove(archie);
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

        [HttpPost]
        public ActionResult UpdateUnit()
        {
            string returnPath = Request.Form[FormData.ReturnPath.ToString()];
            if (returnPath == null || !returnPath.StartsWith("/"))
            {
                return RedirectToAction("Index");
            }
            SetCookie();
            return Redirect(returnPath);
        }

        private void SetCookie()
        {
            try
            {
                preferredUnit = (Units)Enum.Parse(typeof(Units), Request.Form[FormData.Unit.ToString()]);
            }
            catch (Exception)
            {
                return;
            }
            HttpCookie UnitCookie = new HttpCookie(unitCookieName);
            UnitCookie.Expires = DateTime.Now.AddYears(1);
            UnitCookie.Value = preferredUnit.ToString();
            Response.Cookies.Add(UnitCookie);
            ViewBag.Unit = preferredUnit;
        }

        private void GetCookie()
        {
            try
            {
                preferredUnit = (Units)Enum.Parse(typeof(Units), Request.Cookies[unitCookieName].Value);
            }
            catch (Exception)
            {
            }
            ViewBag.Unit = preferredUnit;
        }
    }
}
