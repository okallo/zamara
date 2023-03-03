using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Zamara.Models;


public class User :IdentityUser {
    [Required]
    [Key]
    public string Id {get;set;} = new Guid().ToString();
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Policy")]
    public string? Policy {get;set;}
    [Required]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Claims")]
    public string? Claims {get;set;}
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Name")]
    public string? Name {get;set;}
}