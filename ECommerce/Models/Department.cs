using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Display(Name ="Department")]
        [Required(ErrorMessage = "The field {0} is Required.")]
        [StringLength(50, ErrorMessage = "The field {0} must be maximum {1} and an minimum {2} characters length.", MinimumLength = 3)]
        [Index("Department_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        //relaciones:
        public virtual  ICollection<City> Cities { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<WareHouse> WareHouse { get; set; }
    }
}