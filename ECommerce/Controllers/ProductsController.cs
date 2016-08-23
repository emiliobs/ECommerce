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
    [Authorize(Roles = "User")]
    public class ProductsController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: Products
        public ActionResult Index()
        {
            //busco los usuarios logeados:
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            var products = db.Products
                           .Include(p => p.Category)
                           //.Include(p => p.Company)
                           .Include(p => p.Tax)
                           .Where(p => p.CompanyId.Equals(user.CompanyId));

            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            ViewBag.CategoryId = new SelectList(CombosHelper.GetCategories(user.CompanyId), "CategoryId", "Description");
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name");
            ViewBag.TaxId = new SelectList(CombosHelper.GetTaxes(user.CompanyId), "TaxId", "Description");

            //lemando el objeto product a la vista:
            var product = new Product
            {
                CompanyId = user.CompanyId
            };

            return View(product);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            var user = db.Users.Where(u => u.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Products.Add(product);


                try
                {
                    db.SaveChanges();

                    if (product.ImageFile != null)
                    {
                        var file = $"{product.ProductId}.jpg";
                        var folder = "~/Content/Products";

                        var response = FileHelper.UploadPhoto(product.ImageFile, folder, file);

                        if (response)
                        {


                            product.Image = $"{folder}/{file}";

                            db.Entry(product).State = EntityState.Modified;

                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Index");

                }
                catch (Exception)
                {

                    throw;
                }

            }

            ViewBag.CategoryId = new SelectList(CombosHelper.GetCategories(user.CompanyId), "CategoryId", "Description", product.CategoryId);
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", product.CompanyId);
            ViewBag.TaxId = new SelectList(CombosHelper.GetTaxes(user.CompanyId), "TaxId", "Description", product.TaxId);

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            //var user = db.Users.Where(u=>u.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(CombosHelper.GetCategories(product.CompanyId), "CategoryId", "Description", product.CategoryId);
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", product.CompanyId);
            ViewBag.TaxId = new SelectList(CombosHelper.GetTaxes(product.CompanyId), "TaxId", "Description", product.TaxId);

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            //var user = db.Users.Where(u=>u.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            if (ModelState.IsValid)
            {

                if (product.ImageFile != null)
                {
                    var file = $"{product.ProductId}.jpg";
                    var folder = "~/Content/Products";

                    var response = FileHelper.UploadPhoto(product.ImageFile, folder, file);

                    if (response)
                    {


                        product.Image = $"{folder}/{file}";

                        //db.Entry(product).State = EntityState.Modified;

                        //db.SaveChanges();
                    }
                }

                    db.Entry(product).State = EntityState.Modified;

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

                ViewBag.CategoryId = new SelectList(CombosHelper.GetCategories(product.CompanyId), "CategoryId", "Description", product.CategoryId);
                //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", product.CompanyId);
                ViewBag.TaxId = new SelectList(CombosHelper.GetTaxes(product.CompanyId), "TaxId", "Description", product.TaxId);

                return View(product);
            }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);

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
