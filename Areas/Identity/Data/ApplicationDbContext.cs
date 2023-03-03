using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using zamara.Models;
using Zamara.Data;
using Zamara.Models;

namespace zamara.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    private UserManager<User> _userManager;
    private IUserStore<User> _userStore;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        new DbInitializer(builder).Seed();
        User user1 = new User()
        {
            Id = "bc00907f-6b2b-4e2f-bd1c-0dc77965de3b",
            UserName = "jk@zamara.co.ke",
            Name = "James Kimani",
            Email = "jk@zamara.co.ke",
            Policy = "WebPolicy",
            Claims = "Staff,Posts,Reports",
            EmailConfirmed = true,
            NormalizedEmail = "JK@ZAMARA.CO.KE",
            NormalizedUserName = "JK@ZAMARA.CO.KE"

        };
        
       this.SeedUsersAsync(builder, user1, "Test1234");
        User user2 = new User()
        {
            Id = "7e3ebd93-7e28-45e0-b638-7ce99299d850",
            UserName = "sa@zamara.co.ke",
            Name = "Stephen Achieng",
            Email = "sa@zamara.co.ke",
            Policy = "WebPolicy",
            Claims = "Staff,Continents,Reports",
            EmailConfirmed = true,
            NormalizedEmail = "SA@ZAMARA.CO.KE",
            NormalizedUserName = "SA@ZAMARA.CO.KE"
        };
        this.SeedUsersAsync(builder, user2, "Test4567");
        User user3 = new User()
        {
            Id = "d53b4055-8b95-430f-82bb-3cd8e86421be",
            UserName = "so@zamara.co.ke",
            Name = "Samuel Okutoyi",
            Email = "so@zamara.co.ke",
            Policy = "WebPolicy",
            Claims = "Staff,Continents,Reports",
            EmailConfirmed = true,
            NormalizedEmail = "SO@ZAMARA.CO.KE",
            NormalizedUserName = "SO@ZAMARA.CO.KE"

        };
       this.SeedUsersAsync(builder, user3, "Test8901");
        
          
        //   builder.Entity<IdentityUserRole<string>>().HasData(
        //     new IdentityUserRole<string> { 
        //    RoleId = "db7e5c48-b9b9-4225-89d9-9e48dfe12361", 
        //    UserId = "bc00907f-6b2b-4e2f-bd1c-0dc77965de3b"
        //   },
        //     new IdentityUserRole<string> { 
        //    RoleId = "31123878-9202-4d18-b968-14941124892e", 
        //    UserId = "7e3ebd93-7e28-45e0-b638-7ce99299d850"
        //   },
        //     new IdentityUserRole<string> { 
        //    RoleId = "c5119adf-4a5a-41b0-886b-55b12994fceb", 
        //    UserId = "d53b4055-8b95-430f-82bb-3cd8e86421be"
        //   }
        //   );
    }
     public  void SeedUsersAsync(ModelBuilder builder, User user, string password)
    {
       
       var userr = CreateUser(user);
      IdentityUser usere = new IdentityUser { UserName = user.Email };
     // IdentityResult result = await _userManager.CreateAsync(usere, password);
        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        user.PasswordHash = passwordHasher.HashPassword(userr, password);
        builder.Entity<User>().HasData(user);
    }

    private User CreateUser(User user)
    {
        try
            {
                return Activator.CreateInstance<User>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. " +
                    $"Ensure that '{nameof(User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
    }

    public DbSet<Staff> Staff { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<StaffFile> StaffFiles{get;set;}
}
