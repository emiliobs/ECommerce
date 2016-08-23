using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [DataType(DataType.EmailAddress)]
        [Index("User_UserName_Index", IsUnique = true)]
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "The field {0} is Required.")]
        [StringLength(256, ErrorMessage = "The field {0} must be maximum {1} and an minimum {2} characters length.", MinimumLength = 1)]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "The field {0} is Required.")]
        [StringLength(50, ErrorMessage = "The field {0} must be maximum {1} and an minimum {2} characters length.", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "The field {0} is Required.")]
        [StringLength(50, ErrorMessage = "The field {0} must be maximum {1} and an minimum {2} characters length.", MinimumLength = 3)]
        public string LastName { get; set; }

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
        public string Photo { get; set; }

        [Required(ErrorMessage = "The field {0} is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "The field {0} is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }


        [Required(ErrorMessage = "The field {0} is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]
        [Display(Name = "City")]
        public int CityId { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
               return $"{FirstName} { LastName}";
            }

        }


        [NotMapped]//no lo tiene encuenta para enviarlo a la base de datos = no persistencia:
        public HttpPostedFileBase PhotoFile { get; set; }

        //realcio lado varios:

        public virtual Company Company { get; set; }

        public virtual Department Department { get; set; }
        public virtual City City { get; set; }


    }
}