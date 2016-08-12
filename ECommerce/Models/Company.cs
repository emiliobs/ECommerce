using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Display(Name = "Company")]
        [Required(ErrorMessage = "The field {0} is Required.")]
        [StringLength(50, ErrorMessage = "The field {0} must be maximum {1} and an minimum {2} characters length.", MinimumLength = 3)]
        [Index("Company_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is Required.")]
        [StringLength(20, ErrorMessage = "The field {0} must be maximum {1} and an minimum {2} characters length.", MinimumLength = 3)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The field {0} is Required.")]
        [StringLength(100, ErrorMessage = "The field {0} must be maximum {1} and an minimum {2} characters length.", MinimumLength = 1)]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        //La ruta:
        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }


        [Required(ErrorMessage = "The field {0} is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }

        [NotMapped]//no lo tiene encuenta para enviarlo a la base de datos = no persistencia:
        public HttpPostedFileBase LogoFile { get; set; }

        //realcio lado varios:
        public virtual Department Department { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}