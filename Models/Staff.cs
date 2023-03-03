using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Zamara.Models;
[Keyless]
public class Staff : IdentityUser
{
    [DataType(DataType.Upload)]
    [Display(Name = "Photo")]
    public string? Photo { get; set; }
    
    [DataType(DataType.Text)]
    [Display(Name = "Department")]
    public string? Department { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Salary")]
    public string? Salary { get; set; }
  
    [DataType(DataType.Text)]
    [Display(Name = "StaffNumber")]
    public string? StaffNumber { get; set; }
    
    [DataType(DataType.Text)]
    [Display(Name = "StaffName")]
    public string? Name { get; set; }

   
    [DataType(DataType.Text)]
    [Display(Name = "Policy")]
    public string? Policy { get; set; }
    
    [DataType(DataType.MultilineText)]
    [Display(Name = "Claims")]
    public string? Claims { get; set; }


}