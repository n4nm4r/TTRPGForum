using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TTRPGForum.Data;
using TTRPGForum.Models;

namespace TTRPGForum.Controllers
{
    //Only logged in user can access
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly TTRPGForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(TTRPGForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Comments/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["DiscussionId"] = id;
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Content,CreateDate,DiscussionId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                comment.CreateDate = DateTime.Now;
                comment.ApplicationUserId = user.Id; // Ensure the ApplicationUserId is set

                _context.Add(comment);
                await _context.SaveChangesAsync();

                return RedirectToAction("GetDiscussion", "Home", new { id = comment.DiscussionId });
            }
            ViewData["DiscussionId"] = new SelectList(_context.Discussion, "DiscussionId", "DiscussionId", comment.DiscussionId);
            return View(comment);
        }
    }
}
