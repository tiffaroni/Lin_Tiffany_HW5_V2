using System.ComponentModel.DataAnnotations;
using Lin_Tiffany_HW5_V2.Models;

namespace Lin_Tiffany_HW5_V2.Models
{
    public class OrderDetail
    {
        public Int32 OrderDetailID { get; set; }

        [Required(ErrorMessage = "You must specify a quantity")]
        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000")]
        public Int32 Quantity { get; set; }

        [Display(Name = "Product Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal ProductPrice { get; set; }

        [Display(Name = "Extended Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal ExtendedPrice { get; set; }

        //ADD NAVIGATIONAL PROPERTIES

        // each orderdetail can only have one order 
        public Order Order { get; set; }

        // each orderdetail can only have one product
        public Product Product { get; set; }
    }
}
