using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace HW5Example.Models
{
    public class AppUser : IdentityUser
    {
        //Add additional user fields here
        //First name is provided as an example
        [Display(Name = "First Name")]
        public String FirstName { get; set; }


        [Display(Name = "Last Name")]
        public String LastName { get; set; }


        [Display(Name = "Full Name")]
        public String FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
