using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace MyShop.Core.Models
{
    public class product :BaseEntity
    { 
       
        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0,1000)]
        public decimal price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

       

    }
}
