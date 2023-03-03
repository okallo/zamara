using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using zamara.Data;
using zamara.IService;
using zamara.Models;
using Zamara.Models;

namespace zamara.Controllers;


public class StaffController : Controller
{
    private readonly SignInManager<Staff>_signInManager;
    private readonly UserManager<Staff>_userManager;
    private readonly IUserStore<Staff>_userStore;
    private readonly IUserEmailStore<Staff>_emailStore;
    private readonly ILogger<StaffController> _logger;
    private readonly IEmailSender _emailSender;
    private readonly ApplicationDbContext _context;

    private readonly IStaffService _staffService;

    public StaffController(
        UserManager<Staff>userManager,
        IUserStore<Staff>userStore,
        SignInManager<Staff>signInManager,
        ILogger<StaffController> logger,
        IEmailSender emailSender, ApplicationDbContext context,IStaffService staffService )
    {
        _userManager = userManager;
        _userStore = userStore;
        _emailStore = GetEmailStore();
        _signInManager = signInManager;
        _logger = logger;
        _emailSender = emailSender;
        _staffService = staffService;
        _context = context;
    }

    private IUserEmailStore<Staff>GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }
        return (IUserEmailStore<Staff>)_userStore;
    }

    private Staff CreateUser()
    {
        try
        {
            return Activator.CreateInstance<Staff>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(Staff)}'. " +
                $"Ensure that '{nameof(Staff)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        }
    }
    public IActionResult CreateStaff()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateStaff(StaffDto model)
    {
        
       var result = await _staffService.CreateStaff(model);

        return RedirectToAction("Staff", "Home");
       

    }
    [HttpPost]
    public async Task<ActionResult> DeleteStaff(string id){
        var d = await _staffService.DeleteStaffAsync(id);
        return RedirectToAction("Index", "Home");
    }

    

    public async Task<IActionResult> UploadToDatabase(IFormFile file)

    {
        // foreach (var file in file)
        // {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName);
            var fileModel = new StaffFile
            {
                FileType = file.ContentType,
                Extension = extension,
                Name = fileName,
            };
            using (var dataStream = new MemoryStream())
            {
                await file.CopyToAsync(dataStream);
                fileModel.Data = dataStream.ToArray();
            }
            _context.StaffFiles.Add(fileModel);
            _context.SaveChanges();
        //}

        TempData["Message"] = "File successfully uploaded to Database";
        return RedirectToAction("Index", "Home");
    }
}