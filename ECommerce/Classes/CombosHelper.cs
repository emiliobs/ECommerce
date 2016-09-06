using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace ECommerce.Classes
{
    public class CombosHelper : IDisposable
    {
        private static ECommerceContext db = new ECommerceContext();


        public static List<Department> GetDepartment()
        {
            var departments = db.Departments.ToList();

            departments.Add(new Department
            {
                DepartmentId = 0,
                Name = "[Select a Department.....]"
            });

            return departments.OrderBy(d=>d.Name).ToList();
        }

        public static List<Product> GetProduct(int companyId, bool sw)
        {
            var products = db.Products.Where(p => p.CompanyId == companyId).ToList();

            return products.OrderBy(p=>p.Description).ToList();


        }

        public static List<Product> GetProduct(int companyId)
        {
            var products = db.Products.Where(p=>p.CompanyId.Equals(companyId)).ToList();

            products.Add(new Product {
                    ProductId = 0,
                    Description = "[Select a Product.....]"
            });

            return products.OrderBy(p=>p.Description).ToList();
        }

        public static List<City> GetCities(int deparmentId)
        {
            var cities= db.Cities.Where(c=>c.DepartmentId == deparmentId).ToList();

            cities.Add(new City
            {
                CityId = 0,
                Name = "[Select a City.....]"
            });

            return cities.OrderBy(c => c.Name).ToList();
        }


        public static List<Company> GetCompanies()
        {
            var companies = db.Companies.ToList();

            //companies.Add(new Company
            //{
            //    CompanyId = 0,
            //    Name = "[Select a Company.....]"
            //});

            return companies.OrderBy(c => c.Name).ToList();
        }

       public static List<Category> GetCategories(int companyId)
        {
            var categories = db.Categories.Where(c=>c.CompanyId.Equals(companyId)).OrderBy(d=>d.Description).ToList();

            categories.Add(new Category
            {
                CategoryId = 0,
                Description = "[Select a Category]"
            }
            );

            return categories;
        }

        public static List<Customer> GetCustomer(int companyId)
        {

            var query = (from cu in db.Customers
                         join cc in db.CompanyCustomers on cu.CustomerId equals cc.CustomerId
                         join co in db.Companies on cc.CompanyId equals co.CompanyId
                         where co.CompanyId == companyId
                         select new {cu}).ToList();



            var customers = new List<Customer>();

            foreach (var item in query)
            {
                customers.Add(item.cu);
            }

            //customers.Add(new Customer
            //{
            //    CustomerId = 0,
            //    FirstName = "[Select a Customer.....]"
            //});

            return customers.OrderBy(c=>c.FirstName).ThenBy(c=>c.LastName).ToList();
        }

        public static List<Tax> GetTaxes(int companyId)
        {
            var taxes = db.Taxes.Where(c => c.CompanyId.Equals(companyId)).OrderBy(d => d.Description).ToList();

            taxes.Add(new Tax
            {
                TaxId = 0,
                Description ="[Select a Taxes]"
            });

            return taxes;
        }


        public void Dispose()
        {
            db.Dispose();
        }


    }
}