using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Index("Category_CompanyId_Description_Index", 2, IsUnique = true)]//Indices compuestos:department
        [Required(ErrorMessage = "The field {0} is Required.")]
        //[Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]   
        [StringLength(50, ErrorMessage ="The field {0} must be maximun {1} and minimun {2} characters length", MinimumLength = 1)]
        [Display(Name = "Category")]     
        public string Description { get; set; }

        [Index("Category_CompanyId_Description_Index", 1, IsUnique = true)]//Indices compuestos:department
        [Required(ErrorMessage = "The field {0} is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }


        //Relación:
        public virtual Company Company { get; set; }
    }
}