using System.ComponentModel.DataAnnotations;

namespace Sample.Web.Areas.Account.Models.Access
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}