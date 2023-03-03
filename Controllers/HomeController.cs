using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using zamara.Models;
using Zamara.IService;
using System.Text;
using Zamara.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using zamara.Data;

namespace zamara.Controllers;

public class HomeController : Controller
{
    private static readonly HttpClient httpClient = new HttpClient();
    private readonly ILogger<HomeController> _logger;
    private readonly IPostsService _postsService; private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserStore<IdentityUser> _userStore;

    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, IPostsService postsService, UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,ApplicationDbContext context)
    {
        _logger = logger;
        _postsService = postsService;
        _userManager = userManager;
        _userStore = userStore;
        _signInManager = signInManager;
        _logger = logger;
        _context = context;
    }


    public IActionResult Index()
    {
        return View();
    }
public IActionResult Staff()
    {
        var d = _context.Staff.ToList();
        return View(d);
    }
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(StaffDto model)
    {
        if (ModelState.IsValid)
        {
            var user = new Staff
            {
                UserName = model.Email,
                Email = model.Email,

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

            }
        }

        // If we got this far, something failed, redisplay form
        return View(model);
    }
    public async Task<IActionResult> ContinentsAsync()
    {

        string url = "http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso";

        string xmlSOAP = @"<?xml version=""1.0"" encoding=""utf-8""?>
        <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
            <soap:Body>
            <ListOfContinentsByName xmlns=""http://www.oorsprong.org/websamples.countryinfo""/>         
            </soap:Body>
        </soap:Envelope>";

        try
        {
            string result = await PostSOAPRequestAsync(url, xmlSOAP);

            Console.WriteLine(result);
            return Content(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Content(ex.Message);
        }

    }
    public async Task<IActionResult> Posts()
    {
        var model = await _postsService.GetPosts();
        Console.WriteLine(model.Posts.ToString());
        return View(model);

    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private static async Task<string> PostSOAPRequestAsync(string url, string text)
    {
        using (HttpContent content = new StringContent(text, Encoding.UTF8, "text/xml"))
        using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url))
        {
            request.Headers.Add("SOAPAction", "");
            request.Content = content;
            using (HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                //response.EnsureSuccessStatusCode(); // throws an Exception if 404, 500, etc.
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
