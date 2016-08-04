using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Name { get; set; }

        public virtual  ICollection<City> Cities { get; set; }
    }
}