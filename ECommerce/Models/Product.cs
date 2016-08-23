using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Product
    {
        [Key]

        public int ProductId { get; set; }
       

        [Index("Product_CompanyId_BarCode_Index", 2, IsUnique = true)]//Indices compuestos:
        [Required(ErrorMessage = "The field {0} is Required.")]
        //[Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]   
        [StringLength(100, ErrorMessage = "The field {0} must be maximun {1} and minimun {2} characters length", MinimumLength = 1)]
        [Display(Name = "Product")]
        public string Description  {  get;     set;    }

        [Index("Product_CompanyId_Description_Index", 2, IsUnique = true)]
        [Index("Product_CompanyId_Description_Index", 2, IsUnique = true)]
        [Required(ErrorMessage = "The field {0} is Required.")]
        //[Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]   
        [StringLength(50, ErrorMessage = "The field {0} must be maximun {1} and minimun {2} characters length", MinimumLength = 1)]
        [Display(Name = "Bar Code")]
        public string BarCode { get; set; }

        [Index("Product_CompanyId_Description_Index", 1, IsUnique = true)]//Indices compuestos:department
        [Index("Product_CompanyId_BarCode_Index", 1, IsUnique = true)]//Indices compuestos:department
        [Required(ErrorMessage = "The field {0} is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "The field {0} is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }


        [Required(ErrorMessage = "The field {0} is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]
        [Display(Name = "Tax")]
        public int TaxId { get; set; }

        [Required(ErrorMessage = "The field {0} is Required.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Range(0, double.MaxValue, ErrorMessage = "You must Select a {0} between {1} and {2}")]
        public decimal Price{ get; set;  }
              
        [DataType(DataType.MultilineText)]
        public string Remarks  { get; set; }


        //La ruta:
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [NotMapped]//no lo tiene encuenta para enviarlo a la base de datos = no persistencia:
        [Display(Name ="Image")]
        public HttpPostedFileBase ImageFile { get; set; }

        //Relación:
        public virtual Company Company { get; set; }
        public virtual Category Category { get; set; }
        public virtual Tax Tax { get; set; }
    }
}