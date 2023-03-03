using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Zamara.Models;
[Keyless]
public class Staff : IdentityUser
{
    [Required]
    [DataType(DataType.Upload)]
    [Display(Name = "Photo")]
    public string? Photo { get; set; }
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Department")]
    public string? Department { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    [Display(Name = "Salary")]
    public string? Salary { get; set; }
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "StaffNumber")]
    public string? StaffNumber { get; set; }
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "StaffName")]
    public string? Name { get; set; }

}