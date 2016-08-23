using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class WareHouse
    {
        [Key]
        public int WareHouseId { get; set; }

        [Index("WareHouse_CompanyId_Company_Index", 1, IsUnique = true)]
        [Required(ErrorMessage = "The field {0} is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        
        [Index("WareHouse_CompanyId_Company_Index", 2, IsUnique = true)]
        [Display(Name ="WareHouse")]
        [Required(ErrorMessage = "The field {0} is Required.")]
        [StringLength(50, ErrorMessage = "The field {0} must be maximum {1} and an minimum {2} characters length.", MinimumLength = 1)]
        public string Name { get; set; }
                

        [Required(ErrorMessage = "The field {0} is Required.")]
        [StringLength(20, ErrorMessage = "The field {0} must be maximum {1} and an minimum {2} characters length.", MinimumLength = 3)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The field {0} is Required.")]
        [StringLength(100, ErrorMessage = "The field {0} must be maximum {1} and an minimum {2} characters length.", MinimumLength = 1)]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        
        [Required(ErrorMessage = "The field {0} is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

       

        [Required(ErrorMessage = "The field {0} is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]       
        [Display(Name = "City")]
        public int CityId { get; set; }


        //realcio lado varios:

        public virtual Company Company { get; set; }

        public virtual Department Department { get; set; }
        public virtual City City { get; set; }
    }
}