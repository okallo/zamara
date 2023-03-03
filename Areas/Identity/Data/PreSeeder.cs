using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using zamara.Data;
using Zamara.Models;

namespace Zamara.Data;

public class Preseeder
{

    private UserManager<User> _userManager;
    private IUserStore<User> _userStore;
    private readonly ApplicationDbContext _dbContext;
    private readonly RoleManager<IdentityRole> _roleManager;
    private ModelBuilder modelBuilder;

    public Preseeder(ModelBuilder modelBuilder, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext,IUserStore<User> userStore,UserManager<User> userManager)
    {
        this.modelBuilder = modelBuilder;
        _roleManager = roleManager;
        _dbContext = dbContext;
        _userStore = userStore;

        _userManager = userManager;
    }

    

    public async void AddUserAsync(User model, string password, string role)
    {

        try
        {
            await _userManager.CreateAsync(model, password);

            await _userManager.AddToRoleAsync(model, role);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    //  private async Task SeedUsersAsync()
    // {
    //     if (_dbContext.Users.Any())
    //     {
    //         return;
    //     }
    //     User user1 = new User()
    //     {
    //         Id = "bc00907f-6b2b-4e2f-bd1c-0dc77965de3b",
    //         UserName = "jk@zamara.co.ke",
    //         Name = "James Kimani",
    //         Email = "jk@zamara.co.ke",
    //         Policy = "WebPolicy",
    //         Claims = "Staff,Posts,Reports",
    //         EmailConfirmed = true,
    //         NormalizedEmail = "JK@ZAMARA.CO.KE",
    //         NormalizedUserName = "JK@ZAMARA.CO.KE"

    //     };


    //     await _userManager.CreateAsync(user1, "Test1234");

    //     await _userManager.AddToRoleAsync(user1, "User2");
    //     //this.SeedUsersAsync(builder, user1, "Test1234");
    //     User user2 = new User()
    //     {
    //         Id = "7e3ebd93-7e28-45e0-b638-7ce99299d850",
    //         UserName = "sa@zamara.co.ke",
    //         Name = "Stephen Achieng",
    //         Email = "sa@zamara.co.ke",
    //         Policy = "WebPolicy",
    //         Claims = "Staff,Continents,Reports",
    //         EmailConfirmed = true,
    //         NormalizedEmail = "SA@ZAMARA.CO.KE",
    //         NormalizedUserName = "sa@ZAMARA.CO.KE"
    //     };
    //     this.SeedUsersAsync(builder, user2, "Test4567");
    //     User user3 = new User()
    //     {
    //         Id = "d53b4055-8b95-430f-82bb-3cd8e86421be",
    //         UserName = "so@zamara.co.ke",
    //         Name = "Samuel Okutoyi",
    //         Email = "so@zamara.co.ke",
    //         Policy = "WebPolicy",
    //         Claims = "Staff,Continents,Reports",
    //         EmailConfirmed = true,
    //         NormalizedEmail = "SO@ZAMARA.CO.KE",
    //         NormalizedUserName = "SO@ZAMARA.CO.KE"

    //     };
    //     this.SeedUsersAsync(builder, user3, "Test8901");
    // }

}