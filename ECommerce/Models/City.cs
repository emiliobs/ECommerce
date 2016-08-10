using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required(ErrorMessage ="The field {0} is required.")]
        [Display(Name ="City")]
        [StringLength(50, ErrorMessage = "The field {0} must be maximum {1} and an minimum {2} charactered length.",MinimumLength =3)]
        public string Name { get; set; }

        [Required(ErrorMessage ="The field {0} is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "You must Select a {0}")]
        public int DepartmentId { get; set; }

        //Relación:
        public virtual Department Department { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }
}