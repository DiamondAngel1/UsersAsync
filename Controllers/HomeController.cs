using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Data;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
namespace MyMvcApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Contexta _context;
    public HomeController(ILogger<HomeController> logger, Contexta context)
    {
        _logger = logger;
        _context = context;
        if (!_context.Users.Any()){
            Manager dbm = new Manager();
            dbm.AddUsers(_context);
        }
    }

    public async Task<IActionResult> Index()
    {
        
        List<User> list = await GetListUsersAsync();
        return View(list);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user, IFormFile image)
    {
        if (image != null && image.Length > 0){
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(folderPath)){
                Directory.CreateDirectory(folderPath);
            }
            var fileName = Guid.NewGuid().ToString() ;
            

            using var stream = image.OpenReadStream();
            using var newImage = await Image.LoadAsync(stream);

            var sizes = new[] { 100, 200, 400, 600, 800, 1200 };
            foreach (var size in sizes)
            {
                var resized = newImage.Clone(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(size, size),
                    Mode = ResizeMode.Max
                }));
                var fileName2 = $"{fileName}_{size}px.webp";
                var filePath = Path.Combine(folderPath, fileName2);
                using (var output = System.IO.File.Create(filePath))
                {
                    await resized.SaveAsWebpAsync(output);
                }

            }
            user.Image = $"{fileName}_100px.webp";

        }
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    private Task<List<User>> GetListUsersAsync(){
        return Task.Run(()=>{
            
            return _context.Users.ToList();
        });
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
