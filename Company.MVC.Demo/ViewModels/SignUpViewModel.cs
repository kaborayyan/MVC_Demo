using System.ComponentModel.DataAnnotations;

namespace Company.MVC.Demo.ViewModels
{
	public class SignUpViewModel
	{
        [Required(ErrorMessage ="A user name is required.")]
        public string UserName { get; set; }
        
        
        [Required(ErrorMessage ="Your first name is required.")]
        public string FirstName { get; set; }
        
        
        [Required(ErrorMessage ="Your last name is required.")]
        public string LastName { get; set; }
        
        
        [Required(ErrorMessage ="An email is required.")]
        [EmailAddress(ErrorMessage ="Invalid email.")]
        public string Email { get; set; }
        
        
        [Required(ErrorMessage ="Your password is required.")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage ="The Password must be 4 characters or more.")]
        public string Password { get; set; }
        
        
        [Required(ErrorMessage ="Your password is required.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage ="Both must be the same")]
        public string ConfirmPassword { get; set; }
        
        
        [Required(ErrorMessage ="Your agreement is required.")]
        public bool IsAgree { get; set; }
    }
}
