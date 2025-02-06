using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TTRPGForum.Models;


namespace TTRPGForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //view all Discussion
        public IActionResult Index()
        {
            List<Discussion> discussions = new List<Discussion>();
            Discussion tempDiscussion1 = new Discussion();
            tempDiscussion1.DiscussionId = 1;
            tempDiscussion1.Title = "Dungeon and dragon still the best";
            tempDiscussion1.Content = " This is temporary content for tests 1 <3";
            tempDiscussion1.CreateDate = DateTime.Now;
            tempDiscussion1.ImageFilename = "tempImage.jpg";

            Discussion tempDiscussion2 = new Discussion();
            tempDiscussion2.DiscussionId = 2;
            tempDiscussion2.Title = "Love kids on Bike";
            tempDiscussion2.Content = " This is temporary content for tests 2 <3";
            tempDiscussion2.CreateDate = DateTime.Now;
            tempDiscussion2.ImageFilename = "tempImage.jpg";

            Discussion tempDiscussion3 = new Discussion();
            tempDiscussion3.DiscussionId = 3;
            tempDiscussion3.Title = "Roots seems fun. Thoughts?";
            tempDiscussion3.Content = " This is temporary content for tests 3 <3";
            tempDiscussion3.CreateDate = DateTime.Now;
            tempDiscussion3.ImageFilename = "tempImage.jpg";

            discussions.Add(tempDiscussion1);
            discussions.Add(tempDiscussion2);
            discussions.Add(tempDiscussion3);



            return View(discussions);
        }

        

        public IActionResult GetDiscussion(int id)
        {
        
            Discussion tempDiscussion = new Discussion();
            tempDiscussion.DiscussionId = id;
            tempDiscussion.Title = "Dungeon and dragon woo";
            tempDiscussion.Content = " This is temporary content for tests for the discussion details <3";
            tempDiscussion.CreateDate = DateTime.Now;
            tempDiscussion.ImageFilename = "tempImage.jpg";

            return View(tempDiscussion);
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
}
