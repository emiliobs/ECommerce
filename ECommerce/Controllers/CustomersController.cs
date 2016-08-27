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
    public class CustomersController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: Customers
        public ActionResult Index()
        {
            var user = db.Users.Where(u=>u.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            var customers = db.Customers.Where(c=>c.CompanyId.Equals(user.CompanyId)).Include(c => c.City).Include(c => c.Department);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            var customer = new Customer
            {
                CompanyId = user.CompanyId
            };

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(), "CityId", "Name");
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name");
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDepartment(), "DepartmentId", "Name");
            return View(customer);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);

                try
                {
                    db.SaveChanges();
                    UserHelper.CreateUserASP(customer.UserName,"Customer");

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty,  ex.Message);
                }

                
            }

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(), "CityId", "Name", customer.CityId);
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", customer.CompanyId);
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDepartment(), "DepartmentId", "Name", customer.DepartmentId);

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(), "CityId", "Name", customer.CityId);
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", customer.CompanyId);
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDepartment(), "DepartmentId", "Name", customer.DepartmentId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();

                    //TO DO: validate when tue customer email change:

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    throw;
                }

                
            }

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(), "CityId", "Name", customer.CityId);
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", customer.CompanyId);
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDepartment(), "DepartmentId", "Name", customer.DepartmentId);

            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);


            db.Customers.Remove(customer);

            try
            {
                db.SaveChanges();

                UserHelper.DeleteUser(customer.UserName);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }

           
        }
        public JsonResult GetCities(int departmentId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cities = db.Cities.Where(c => c.DepartmentId == departmentId);

            return Json(cities);
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
