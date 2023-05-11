using JokesMVC2023.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JokesMVC2023.Controllers
{
    public class FavouriteController : Controller
    {
        private readonly JokeDBContext _context;

        public FavouriteController(JokeDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int? id = HttpContext?.Session?.GetInt32("ID");
            if(!id.HasValue)
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        public async Task<IActionResult> GetFavouriteListDDL()
        {
            // retrieve ID from session
            int? id = HttpContext?.Session?.GetInt32("ID");
            if(!id.HasValue)
            {
                return Unauthorized();
            }

            // Use the ID to get all favourite lists
            var betterList =  _context.FavouriteLists.Where(c => c.UserId == id).ToList();
            var selectList = betterList.Select(c => new SelectListItem{
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            // send favourite lists (as a selectlist) to view
            ViewBag.FavouriteLists = selectList;

            // Create a partial view that renders a DDL
            return PartialView("_FavouriteListDDL");
        }

        [HttpPost]
        public async Task<IActionResult> AddNewList([FromBody] string listName)
        {

            // retrieve ID from session
            int? id = HttpContext?.Session?.GetInt32("ID");
            if (!id.HasValue)
            {
                return Unauthorized();
            }

            if(_context.FavouriteLists.Any(c => c.Name == listName && c.UserId == id)) 
            {
                return BadRequest();
            }

            FavouriteList newList = new FavouriteList()
            {
                Name = listName,
                UserId = id.Value
            };
            _context.FavouriteLists.Add(newList);
            _context.SaveChanges();

            return Ok();
        }
    
    }
}
