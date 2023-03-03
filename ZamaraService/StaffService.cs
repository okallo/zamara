using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using zamara.Data;
using zamara.IService;
using zamara.Models;
using Zamara.Models;

namespace zamara.Service;

public class StaffService : IStaffService
{
    private readonly SignInManager<Staff> _signInManager;
    private readonly UserManager<Staff> _userManager;
    private readonly IUserStore<Staff> _userStore;
    private readonly IUserEmailStore<Staff> _emailStore;
    private readonly ILogger<StaffDto> _logger;
    private readonly IEmailSender _emailSender;
    private readonly ApplicationDbContext _context;

    //private  readonly RoleManager<IdentityRole> _roleManager; 
    private readonly IPasswordHasher<Staff> _passwordHasher;

    public StaffService(UserManager<Staff> userManager,
            IUserStore<Staff> userStore,
            SignInManager<Staff> signInManager,
            ILogger<StaffDto> logger,
            IEmailSender emailSender, IPasswordHasher<Staff> passwordHasher,ApplicationDbContext context)
    {
        _userManager = userManager;
        _userStore = userStore;
        _emailStore = GetEmailStore();
        _signInManager = signInManager;
        _logger = logger;
        _emailSender = emailSender;
        _passwordHasher = passwordHasher;
        _context = context;
        //_roleManager = roleManager;RoleManager<IdentityRole> roleManager
    }

    private IUserEmailStore<Staff>? GetEmailStore()
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

    public async Task<Staff> CreateStaff(StaffDto staff)
    {


        ///IFormFile file = staff.PhotoFile;
        
        var fileName = Path.GetFileNameWithoutExtension(staff.PhotoFile.Name);
            var extension = Path.GetExtension(staff.PhotoFile.Name);
            var fileModel = new StaffFile
            {
                FileType = staff.PhotoFile.ContentType,
                Extension = extension,
                Name = fileName
            };
            using (var dataStream = new MemoryStream())
            {
                await staff.PhotoFile.CopyToAsync(dataStream);
                fileModel.Data = dataStream.ToArray();
            }
            _context.StaffFiles.Add(fileModel);
            _context.SaveChanges();
           var fileId =  _context.StaffFiles.Where(r => r.Name == fileName).FirstOrDefault();
           
        Staff st = new Staff();
        try
        {
            st = CreateUser();
            st.Claims = staff.Claims;
            st.Department = staff.Department;
            st.Email = staff.Email;
            st.EmailConfirmed = true;
            st.Name = staff.Name;
            st.UserName = staff.UserName;
            st.NormalizedEmail = staff.Email.ToUpper();
            st.NormalizedUserName = staff.Email.ToUpper();
            st.Policy = staff.Policy;
            st.Photo = fileId.Name;
            st.StaffNumber = staff.StaffNumber;

            await _userStore.SetUserNameAsync(st, staff.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(st, staff.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(st, staff.Password);
            if (result.Succeeded)
            {



                _logger.LogInformation("User created a new account with password.");

                var userId = await _userManager.GetUserIdAsync(st);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(st);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                await _emailSender.SendEmailAsync(staff.Email, "Confirm your email",
                    $"Greeting {staff.Name}, we are glad to inform you that your staff profile has been created.");
            }
            Console.WriteLine(st);
            return st;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new NotImplementedException();
        }
    }

    public async Task<bool> DeleteStaffAsync(string id)
    {
        try
        {
            Staff user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);



            }
            return true;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

    }

    public async Task<Staff> EditStaffAsync(StaffDto staff)
    {
        try
        {
            StaffDto user = (StaffDto)await _userManager.FindByEmailAsync(staff.Email);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(staff.Email))
                    user.Email = staff.Email;

                if (!string.IsNullOrEmpty(staff.Password))
                    user.PasswordHash = _passwordHasher.HashPassword(user, staff.Password);
                else
                    throw new ArgumentNullException("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(staff.Email) && !string.IsNullOrEmpty(staff.Password))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return user;
                    else
                        throw new ArgumentNullException("An error occured  ", result.ToString());
                }
            }
            else
                throw new ArgumentNullException("", "User Not Found");

            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new ArgumentNullException("This error occured ", ex.Message);
        }
    }

    public List<Staff> GetAllStaff()
    {
        var d = _userManager.Users.Where(r => r.StaffNumber != null).ToList();
        List<Staff> staff = new List<Staff>();
        Staff staf = new Staff();
        foreach (var item in d)
        {
            staf = item;
            staff.Add(staf);
        }
        return staff;
    }

    public Task<Staff> GetStaff(string id)
    {
        var d = _userManager.Users.Where(r => r.Id == id);
        return (Task<Staff>)d;
    }


}