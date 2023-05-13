using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<AccountController> _logger;
    
    public AccountController(
        SignInManager<User> signInManager, 
        ILogger<AccountController> logger,
        UserManager<User> userManager
        )
    {
        _signInManager = signInManager;
        _logger = logger;
        _userManager = userManager;
    }
    
    //login
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost("/Account/Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return Redirect("/");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");

        return View(model);
    }
    
    [HttpPost("/Account/Logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/");
    }
    
    //register
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost("/Account/Register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        // if (!ModelState.IsValid) return View(model);
        var user = new User { UserName = model.Name };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return Redirect("/");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        
        if(!result.Succeeded)
            foreach(IdentityError error in result.Errors)
                Console.WriteLine($"Oops! {error.Description} ({error.Code})");

        return View(model);
    }
}