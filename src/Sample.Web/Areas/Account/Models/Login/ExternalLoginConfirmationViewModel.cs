using System.ComponentModel.DataAnnotations;

namespace Sample.Web.Areas.Account.Models.Login
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}