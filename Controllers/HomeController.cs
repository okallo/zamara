using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using zamara.Models;
using Zamara.IService;

namespace zamara.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPostsService _postsService;

    public HomeController(ILogger<HomeController> logger,IPostsService postsService)
    {
        _logger = logger;
        _postsService = postsService;
    }

    public IActionResult Index()
    {
        return View();
    }
     public async Task<IActionResult> Posts()
    {
        var model = await _postsService.GetPosts();
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
}
