using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
  public class UserProfile
  {
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    public string Email { get; set; }

    [StringLength(100)]
    [Display(Name = "First name")]
    public string FirstName { get; set; }

    [StringLength(100)]
    [Display(Name = "Last name")]
    public string LastName { get; set; }

    public bool? IsAdmin { get; set; }

    // This attribute only exists to hack around the Simple Membership providers hash-only password
    // storage implementation. Never, ever, ever do this!!!
    [StringLength(200)]
    public string Password { get; set; }
  }

  public class LocalPasswordModel
  {
    [Required]
    [MinLength(14, ErrorMessage = "14 characters minimum you fucking dweeb....")]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm new password")]
    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }

  public class LoginModel
  {
    [Required]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
  }

  public class PasswordResetModel
  {
    [Required]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
  }

  public class RegisterModel
  {
    [Required]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "First name")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Last name")]
    public string LastName { get; set; }

    [Required]
        [MinLength(14, ErrorMessage = "14 characters minimum you fucking dweeb....")]
        [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Z].*)(?=.*[!@#$&*].*)(?=.*[0-9].*)(?=.*[a-z].*).{14,}$", ErrorMessage = "The password must contain a lower case, upper case, digit and special char from !@#$&*\"")]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }
}
