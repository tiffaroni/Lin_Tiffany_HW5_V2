using System.ComponentModel.DataAnnotations;
using Lin_Tiffany_HW5_V2.Models;

namespace Lin_Tiffany_HW5_V2.Models
{
    public enum ProductType
    {
        [Display(Name = "New Hardback")] New_Hardback,
        [Display(Name = "New Paperback")] New_Paperback,
        [Display(Name = "Used Hardback")] Used_Hardback,
        [Display(Name = "Used Paperback")] Used_Paperback,
        Other
    }
    public class Product
    {
        public Int32 ProductID { get; set; }

        [Required(ErrorMessage = "Product name is required!")]
        [Display(Name = "Product Name:")]
        public String Name { get; set; }

        [Display(Name = "Product Description:")]
        public String Description { get; set; }

        [Required(ErrorMessage = "Product price is required!")]
        [Display(Name = "Product Price:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal Price { get; set; }

        [Display(Name = "Product Type:")]
        public ProductType ProductType { get; set; }

        // It will create new (empty) list of Books if the navigational property is null.
        // //This is helpful because you can’t iterate over a null list
        // //You can’t add or remove from a null list, and you can’t count a null list.
        // //Putting this code into a constructor prevents null reference errors throughout the application.

        public Product()
        {
            if (Suppliers == null)
            {
                Suppliers = new List<Supplier>();
            }

            if (OrderDetails == null)
            {
                OrderDetails = new List<OrderDetail>();
            }
        }

        //TODO: ADD NAVIGATIONAL PROPERTIES

        // each product can have many suppliers 
        public List<Supplier> Suppliers { get; set; }

        // each product can have many orderdetails
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
