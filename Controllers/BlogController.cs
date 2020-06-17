using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using assignment_4.Data;
using assignment_4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace assignment_4.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _um;
        
        public BlogController(ApplicationDbContext context, UserManager<ApplicationUser> um)
        {
            _context = context;
            _um = um;
        }

        // GET: Blog
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext =await _context.Posts.Include(p => p.Owner).ToListAsync();

            var model = new PostViewModel()
            {
                Posts = applicationDbContext
            };
            
            return View(model);
        }

        // GET: Blog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Blog/Create
        [HttpGet]
        public IActionResult Add()
        {
            
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,Title,Summary,Content")] Post post)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(post);
                
                var user = _um.GetUserId(User);
                post.OwnerId = user;
                
                post.Time = DateTimeOffset.Now.ToString("dd:MM:yy");
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(post);
        }

        // GET: Blog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser currentUser = null;
            currentUser = await _um.GetUserAsync(User);
            var post = await _context.Posts.FindAsync(id);

            if (currentUser == null)
                return RedirectToAction("Index");
            
            
            if (post.OwnerId!= currentUser.Id)
            {
                return RedirectToAction(nameof(Index)); 
            }

            
            if (post == null)
            {
                return NotFound();
            }
            
            return View(post);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Summary,Content")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _um.GetUserId(User);
                    post.OwnerId = user;
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
       
            return View(post);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
