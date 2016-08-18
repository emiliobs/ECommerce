using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Tax
    {
        [Key]
        public int TaxId { get; set; }

        [Index("Tax_CompanyId_Description_Index", 2, IsUnique = true)]//Indices compuestos:department
        [Required(ErrorMessage = "The field {0} is Required.")]
        //[Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]   
        [StringLength(50, ErrorMessage = "The field {0} must be maximun {1} and minimun {2} characters length", MinimumLength = 1)]
        [Display(Name = "Tax")]
        public string Description { get; set; }


        
        [Required(ErrorMessage = "The field {0} is Required.")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        [Range(0,100, ErrorMessage = "You must Select a {0} between {1} and {2}")]
        public Double Rate {   get;set;  }

        [Index("Tax_CompanyId_Description_Index", 1, IsUnique = true)]//Indices compuestos:department
        [Required(ErrorMessage = "The field {0} is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]
        [Display(Name = "Company")]        
        public int CompanyId { get; set; }



        //Relación:
        public virtual Company Company { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}