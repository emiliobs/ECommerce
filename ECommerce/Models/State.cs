using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Required(ErrorMessage = "The field {0} is Required.")]
        [MaxLength(50,ErrorMessage ="The field {0} must be maximum {1} characters.")]
        [Display(Name ="State")]
        [Index("State_Description_Index", IsUnique = true)]
        public string Description { get; set; }

        //relacion:
        public virtual ICollection<Order> Orders { get; set; }
    }
}