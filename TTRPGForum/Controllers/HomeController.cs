using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TTRPGForum.Data;
using TTRPGForum.Models;
using Microsoft.EntityFrameworkCore;


namespace TTRPGForum.Controllers
{
    public class HomeController : Controller
    {
        public readonly TTRPGForumContext _context;

        private readonly ILogger<HomeController> _logger;

        

        // Constructor
        public HomeController(TTRPGForumContext context)
        {
            _context = context;
        }

        // Home page - all discussions - ../ or ../Home/Index
        public async Task<IActionResult> Index()
        {
            // get a list of all discussions from db
            var discussions = await _context.Discussion
                .OrderByDescending(d => d.CreateDate)
                .Include(d => d.Comments).ToListAsync();

            // pass discussion object into View
            return View(discussions);
        }



        public async Task<IActionResult> GetDiscussion(int id)
        {
            var discussion = await _context.Discussion
                .Include(d => d.Comments) 
                .FirstOrDefaultAsync(d => d.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
