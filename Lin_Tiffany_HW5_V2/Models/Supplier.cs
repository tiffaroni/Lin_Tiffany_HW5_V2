using System.ComponentModel.DataAnnotations;
using Lin_Tiffany_HW5_V2.Models;

namespace Lin_Tiffany_HW5_V2.Models
{
    public class Supplier
    {
        public Int32 SupplierID { get; set; }

        [Required(ErrorMessage = "Supplier name is required")]
        [Display(Name = "Supplier Name")]
        public String SupplierName { get; set; }

        [Required(ErrorMessage = "Supplier email is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Supplier Email")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public String PhoneNumber { get; set; }

        public Supplier()
        {
            if (Products == null)
            {
                Products = new List<Product>();
            }
        }

        //ADD NAVIGATIONAL PROPERTIES

        // each supplier can have many products
        public List<Product> Products { get; set; }

    }
}
