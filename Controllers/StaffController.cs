using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using zamara.Data;
using zamara.Models;
using Zamara.Models;

namespace zamara.Controllers;


public class StaffController: Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserStore<IdentityUser> _userStore;
    private readonly IUserEmailStore<IdentityUser> _emailStore;
    private readonly ILogger<StaffController> _logger;
    private readonly IEmailSender _emailSender;
    private readonly ApplicationDbContext _context;

    public StaffController(
        UserManager<IdentityUser> userManager,
        IUserStore<IdentityUser> userStore,
        SignInManager<IdentityUser> signInManager,
        ILogger<StaffController> logger,
        IEmailSender emailSender,ApplicationDbContext context)
    {
        _userManager = userManager;
        _userStore = userStore;
        _emailStore = GetEmailStore();
        _signInManager = signInManager;
        _logger = logger;
        _emailSender = emailSender;

        _context = context;
    }

    private IUserEmailStore<IdentityUser> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }
        return (IUserEmailStore<IdentityUser>)_userStore;
    }

    public IActionResult CreateStaff()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateStaff(StaffDto model)
    {
        if (ModelState.IsValid)
        {
            var user = new Staff
            {
                Photo = model.Photo,
                Department = model.Department,
                Salary = model.Salary,
                StaffNumber = model.StaffNumber,
                Name = model.Name
            };
            var result = await _userManager.CreateAsync(user, model.Password);

        }
        return Content(model.ToString());

    }

          public async Task<IActionResult> UploadToDatabase(List<IFormFile> files, string description)
        {
            foreach (var file in files)
            {
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
            }
            TempData["Message"] = "File successfully uploaded to Database";
            return RedirectToAction("Index");
        }
}