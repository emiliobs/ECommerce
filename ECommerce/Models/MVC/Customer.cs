using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }


        [Required(ErrorMessage ="The field {0} is Required.")]
        [MaxLength(256, ErrorMessage ="The field {0} must be maximun {1} character lenght.")]
        [Display(Name ="E-Mail")]

        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The field {0} is Required.")]
        [MaxLength(50, ErrorMessage = "The field {0} must be maximun {1} character lenght.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The field {0} is Required.")]
        [MaxLength(50, ErrorMessage = "The field {0} must be maximun {1} character lenght.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The field {0} is Required.")]
        [MaxLength(20, ErrorMessage = "The field {0} must be maximun {1} character lenght.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The field {0} is Required.")]
        [MaxLength(100, ErrorMessage = "The field {0} must be maximun {1} character lenght.")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "The field {0} is Required.")]
        [Range(1,double.MaxValue, ErrorMessage = "You must select a {0}.")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "The field {0} is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}.")]
        [Display(Name = "City")]
        public int CityId { get; set; }

        [Display(Name = "Customer")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }

        }

        //relacion
        public virtual Department Department { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<CompanyCustomer> CompanyCustomers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}