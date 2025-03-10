using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTRPGForum.Data;
using TTRPGForum.Models;

namespace TTRPGForum.Controllers
{
    [Authorize]
    public class DiscussionsController : Controller
    {
        private readonly TTRPGForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DiscussionsController(TTRPGForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Discussions
        public async Task<IActionResult> Index()
        {
            var discussions = await _context.Discussion
                .Include(d => d.ApplicationUser) // Include the ApplicationUser
                .Where(m => m.ApplicationUserId == _userManager.GetUserId(User))
                .ToListAsync();

            return View(discussions);
        }

        // GET: Discussions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion.Include(d => d.Comments)
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null || discussion.ApplicationUserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            return View(discussion);
        }

        // GET: Discussions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Discussions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiscussionId,Title,Content,ImageFile,CreateDate")] Discussion discussion)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                discussion.CreateDate = DateTime.Now;
                discussion.ApplicationUserId = user.Id;
                discussion.ApplicationUser = user; // Set the ApplicationUser

                if (discussion.ImageFile != null)
                {
                    discussion.ImageFilename = Guid.NewGuid().ToString() + Path.GetExtension(discussion.ImageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", discussion.ImageFilename);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await discussion.ImageFile.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    discussion.ImageFilename = "placeholder-image.jpg";
                }

                _context.Add(discussion);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View(discussion);
        }

        // GET: Discussions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion
                .Where(m => m.ApplicationUserId == _userManager.GetUserId(User))
                .Include("Comments")
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null || discussion.ApplicationUserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }
            return View(discussion);
        }

        // POST: Discussions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiscussionId,Title,Content,ImageFilename,CreateDate")] Discussion discussion)
        {
            if (id != discussion.DiscussionId)
            {
                return NotFound();
            }

            if (discussion.ApplicationUserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discussion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionExists(discussion.DiscussionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(discussion);
        }

        // GET: Discussions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null || discussion.ApplicationUserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            return View(discussion);
        }

        // POST: Discussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discussion = await _context.Discussion
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null || discussion.ApplicationUserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            _context.Discussion.Remove(discussion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscussionExists(int id)
        {
            return _context.Discussion.Any(e => e.DiscussionId == id);
        }
    }
}
