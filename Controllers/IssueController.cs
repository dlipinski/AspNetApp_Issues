using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IssueManager.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace core_sec.Controllers
{
    public class IssueController : Controller
    {
        private readonly IssueManagerContext _context;

        public IssueController(IssueManagerContext context)
        {
            _context = context;
        }

        // GET: Issue
        public async Task<IActionResult> Index()
        {
            var mvcMovieContext = _context.Issue.Include(i => i.IssueType);
            return View(await mvcMovieContext.ToListAsync());
        }

        // GET: Issue/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issue
                .Include(i => i.IssueType)
                .SingleOrDefaultAsync(m => m.IssueID == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // GET: Issue/Create
        public IActionResult Create()
        {
            ViewData["IssueTypeID"] = new SelectList(_context.IssueType, "IssueTypeID", "Name");
            return View();
        }

        // GET: Issue/Create
        public IActionResult UserStart()
        {
            ViewData["IssueTypeID"] = new SelectList(_context.IssueType, "IssueTypeID", "Name");
            return View();
        }

        // POST: Issue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IssueID,IsDefined,Content,Solution,State,Created,Solved,CreatorID,SolverID,IssueTypeID")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IssueTypeID"] = new SelectList(_context.IssueType, "IssueTypeID", "Name", issue.IssueTypeID);
            return View(issue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreateNew([Bind("IssueID,IsDefined,Content,Solution,State,Created,Solved,CreatorID,SolverID,IssueTypeID")] Issue issue)
        {
            issue.IsDefined = 1;
            issue.State = 0;
            issue.Solution = "undefined";
            issue.Created = DateTime.Now;
            issue.Solved = DateTime.Now;
            issue.CreatorID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            issue.SolverID = "undefined";
            _context.Add(issue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UserStart));
            
        }

        // GET: Issue/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issue.SingleOrDefaultAsync(m => m.IssueID == id);
            if (issue == null)
            {
                return NotFound();
            }
            ViewData["IssueTypeID"] = new SelectList(_context.IssueType, "IssueTypeID", "Name", issue.IssueTypeID);
            return View(issue);
        }

        // POST: Issue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IssueID,Solution,State,Created,Solved,CreatorID,SolverID,IssueTypeID")] Issue issue)
        {
            if (id != issue.IssueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.IssueID))
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
            ViewData["IssueTypeID"] = new SelectList(_context.IssueType, "IssueTypeID", "Name", issue.IssueTypeID);
            return View(issue);
        }

        // GET: Issue/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issue
                .Include(i => i.IssueType)
                .SingleOrDefaultAsync(m => m.IssueID == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: Issue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await _context.Issue.SingleOrDefaultAsync(m => m.IssueID == id);
            _context.Issue.Remove(issue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
            return _context.Issue.Any(e => e.IssueID == id);
        }
    }
}
