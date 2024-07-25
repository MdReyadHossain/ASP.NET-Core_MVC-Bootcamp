using CrudApp.Constant;
using CrudApp.DTOs;
using CrudApp.Models;
using CrudApp.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace CrudApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext dbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDTO user)
        {
            var existedUser = (from dbUser in dbContext.Users
                               where user.UserName == dbUser.UserName || user.Email == dbUser.Email
                               select dbUser).SingleOrDefault();

            ModelState.Remove("Phone");
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Please fill the input field properly!";
            }
            else if (existedUser != null)
            {
                TempData["error"] = "User has already exist!";
            }
            else if (user.Password == user.RePassword)
            {
                var newUser = new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone ?? "",
                    Password = user.Password,
                    UserName = user.UserName,
                    Status = false,
                    Type = "user"
                };
                await dbContext.Users.AddAsync(newUser);
                await dbContext.SaveChangesAsync();
                TempData["success"] = "User Created Succesful!";

                ModelState.Clear();
                return RedirectToAction(nameof(Login));
            }
            else
            {
                TempData["error"] = "Password not Matched!";
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                var user = await (from dbUser in dbContext.Users
                            where dbUser.UserName == login.Username && 
                                    dbUser.Password == login.Password && 
                                    dbUser.Type.ToUpper() == "ADMIN"
                            select dbUser).SingleOrDefaultAsync();

                if (user != null)
                {
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Sid, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("Phone", user.Phone),
                        new Claim("UserName", user.UserName),
                    };

                    var identity = new ClaimsIdentity(claims, AuthConstant.IDENTITY_AUTH_TYPE);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(AuthConstant.IDENTITY_AUTH_TYPE, claimsPrincipal);

                    TempData["success"] = "Login Successfull!";
                    ModelState.Clear();

                    string returnUrl = HttpContext.Request.Query["returnUrl"];

                    if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("UserList", "Admin");
                }
            }
            TempData["error"] = "Invalid login credential";
            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(AuthConstant.IDENTITY_AUTH_TYPE);
            return RedirectToAction(nameof(Login));
        }
    }
}
