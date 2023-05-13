using ChatApp.Contexts;
using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ChatAppContext _context;
    private readonly UserManager<User> _userManager;

    public HomeController(ILogger<HomeController> logger, ChatAppContext context, UserManager<User> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }
    
    public IActionResult Index()
    {
        var currentUser = _userManager.GetUserAsync(User).Result;
        List<ChatRoom> chatRooms = new List<ChatRoom>();
        if (currentUser != null)
        {
            chatRooms = _context.ChatRooms
                .Where(r => r.Users.Any(u => u.Id == currentUser.Id)).ToList();
        }
        
        var model = new IndexViewModel
        {
            CurrentUser = currentUser,
            ChatRooms = chatRooms
        };
        
        return View(model);
    }
    
    public IActionResult Privacy()
    {
        return View();
    }
}