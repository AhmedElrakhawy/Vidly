using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Vidly.Models;
using Vidly.Repository.IRepository;

namespace Vidly.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountRepository _AccountRepository;

        public HomeController(ILogger<HomeController> logger, IAccountRepository accountRepository)
        {
            _logger = logger;
            _AccountRepository = accountRepository;
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
        public IActionResult Login()
        {

            return View(new User());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            var UserObj = await _AccountRepository.Login(SD.UsersUrl + "Authanication/", user);
            if (UserObj == null)
                return View();
            var Identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            Identity.AddClaim(new Claim(ClaimTypes.Name, UserObj.UserName));
            Identity.AddClaim(new Claim(ClaimTypes.Role, UserObj.Role));
            Identity.AddClaim(new Claim(ClaimTypes.Email, UserObj.Email));
            var principal = new ClaimsPrincipal(Identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            HttpContext.Session.SetString("JwtToken", UserObj.Token);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(new User());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            var Result = await _AccountRepository.Register(SD.UsersUrl + "Register/", user);
            if (Result == false)
                return View();

            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JwtToken", "");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
