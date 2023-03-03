using System.ComponentModel.DataAnnotations;

namespace Zamara.Models;

public class StaffDto : Staff{
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password {get;set;}
  [DataType(DataType.Upload)]
    [Display(Name = "Upload Pic")]
    public IFormFile? PhotoFile { get; set; }
     [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string? ConfirmPassword {get;set;}
}