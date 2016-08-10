using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public void Dispose()
        {
            db.Dispose();
        }
    }
}