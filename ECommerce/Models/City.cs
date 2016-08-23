using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
//NOtas:
// * = Indices compuestos:
namespace ECommerce.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Index("Department_City_Name_Index", 1, IsUnique = true)]//Indices compuestos:department
        [Required(ErrorMessage = "The field {0} is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Index("Department_City_Name_Index", 2 , IsUnique = true)]//Indices compuestos: cty
        [Required(ErrorMessage ="The field {0} is required.")]
        [Display(Name ="City")]
        [StringLength(50, ErrorMessage = "The field {0} must be maximum {1} and an minimum {2} charactered length.",MinimumLength =3)]
        public string Name { get; set; }
                        

        //Relación:
        public virtual Department Department { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<WareHouse> WareHouse { get; set; }
    }
}