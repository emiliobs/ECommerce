using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId  { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }

        [Required]
        public int WareHouseId { get; set; }

        [Required]
        public int ProductId { get; set; }

        //relacion:
        public virtual WareHouse WareHouse { get; set; }
        public virtual Product Product { get; set; }
    }
}