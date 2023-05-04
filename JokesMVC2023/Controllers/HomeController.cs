using JokesMVC2023.Models;
using JokesMVC2023.Models.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JokesMVC2023.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly JokeDBContext _dbContext;

        public HomeController(ILogger<HomeController> logger, JokeDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Authentication

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDTO userLogin)
        {
            var user = _dbContext.Users.Where(c => c.Username == userLogin.Username).FirstOrDefault();

            if(user == null)
            {
                // Add some sort of error!
                return View();
            }

            if(BCrypt.Net.BCrypt.Verify(userLogin.Password, user.PasswordHash))
            {
                // Set the ID in the session
                HttpContext.Session.SetInt32("ID", user.Id);
                // Return to Index
                return RedirectToAction("Index");
            }

            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(LoginDTO userLogin)
        {
            AppUser user = new AppUser()
            {
                Username = userLogin.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userLogin.Password)
            };
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("ID");
            return RedirectToAction("Index");
        }


        #endregion

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
}