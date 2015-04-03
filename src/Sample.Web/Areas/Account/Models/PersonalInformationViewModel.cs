using System.ComponentModel.DataAnnotations;

namespace Sample.Web.Areas.Account.Models
{
    public class PersonalInformationViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "FirstName", Description = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LastName", Description = "Last Name")]
        public string LastName { get; set; }
    }
}