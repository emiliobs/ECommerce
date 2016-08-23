using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Classes;

namespace ECommerce.Controllers
{
    [Authorize(Roles ="User")]
    public class WareHousesController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: WareHouses
        public ActionResult Index()
        {
            //nusco el usuario logiado y miro a compñi pertenece:
            var user = db.Users.Where(u=>u.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            //solo busco las ls bodegas de mi compañia: no todas:
            var wareHouses = db.WareHouses.Where(w=>w.CompanyId.Equals(user.CompanyId)).Include(w => w.City).Include(w => w.Department);
            return View(wareHouses.ToList());
        }

        // GET: WareHouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WareHouse wareHouse = db.WareHouses.Find(id);

            if (wareHouse == null)
            {
                return HttpNotFound();
            }
            return View(wareHouse);
        }

        // GET: WareHouses/Create
        public ActionResult Create()
        {

            var user = db.Users.Where(w => w.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            var werehouse = new WareHouse
            {
                CompanyId = user.CompanyId
            };

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(), "CityId", "Name");
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name");
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDepartment(), "DepartmentId", "Name");

           

            return View(werehouse);
        }

        // POST: WareHouses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( WareHouse wareHouse)
        {
            if (ModelState.IsValid)
            {
                db.WareHouses.Add(wareHouse);

                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    throw;
                }

              
            }

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(), "CityId", "Name", wareHouse.CityId);
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", wareHouse.CompanyId);
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDepartment(), "DepartmentId", "Name", wareHouse.DepartmentId);

            return View(wareHouse);
        }

        // GET: WareHouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WareHouse wareHouse = db.WareHouses.Find(id);

            if (wareHouse == null)
            {
                return HttpNotFound();
            }

           

            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", wareHouse.CityId);
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", wareHouse.CompanyId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", wareHouse.DepartmentId);
            return View(wareHouse);
        }

        // POST: WareHouses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( WareHouse wareHouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wareHouse).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(), "CityId", "Name", wareHouse.CityId);
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", wareHouse.CompanyId);
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetCompanies(), "DepartmentId", "Name", wareHouse.DepartmentId);
            return View(wareHouse);
        }

        // GET: WareHouses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WareHouse wareHouse = db.WareHouses.Find(id);
            if (wareHouse == null)
            {
                return HttpNotFound();
            }
            return View(wareHouse);
        }

        // POST: WareHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WareHouse wareHouse = db.WareHouses.Find(id);
            db.WareHouses.Remove(wareHouse);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
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
