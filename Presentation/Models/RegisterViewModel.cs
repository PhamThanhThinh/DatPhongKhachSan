using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
  public class RegisterViewModel
  {
    [Required]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    [Display(Name = "Xác Nhận Mật Khẩu")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string Name { get; set; }

    [Display(Name = "Số Điện Thoại")]
    public string? PhoneNumber { get; set; }

    public string? RedirectUrl { get; set; }

    public string? Role { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem>? RoleList { get; set; }

  }
}
