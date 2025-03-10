using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TTRPGForum.Data;
using TTRPGForum.Models;
using Microsoft.EntityFrameworkCore;

namespace TTRPGForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly TTRPGForumContext _context;
        private readonly ILogger<HomeController> _logger;

        // Constructor
        public HomeController(TTRPGForumContext context)
        {
            _context = context;
        }

        // Home page - all discussions - ../ or ../Home/Index
        public async Task<IActionResult> Index()
        {
            var discussions = await _context.Discussion
                .OrderByDescending(d => d.CreateDate)
                .Include(d => d.ApplicationUser) // Include the ApplicationUser
                .Include(d => d.Comments) // Include Comments
                .ToListAsync();

            return View(discussions);
        }

        public async Task<IActionResult> GetDiscussion(int id)
        {
            var discussion = await _context.Discussion
                .Include(d => d.Comments)
                    .ThenInclude(c => c.ApplicationUser) // Include ApplicationUser for Comments
                .Include(d => d.ApplicationUser) // Include ApplicationUser for Discussion
                .FirstOrDefaultAsync(d => d.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        // Profile page - displays user profile
        public async Task<IActionResult> UserProfile(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var discussions = await _context.Discussion
                .Where(d => d.ApplicationUserId == userId)
                .OrderByDescending(d => d.CreateDate)
                .Include(d => d.Comments)
                .ToListAsync();

            var viewModel = new ProfileViewModel
            {
                User = user,
                Discussions = discussions
            };

            return View("Profile", viewModel);
        }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
