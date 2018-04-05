using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IssueManager.Models;

namespace core_sec.Controllers
{
    public class IssueTypeController : Controller
    {
        private readonly IssueManagerContext _context;

        public IssueTypeController(IssueManagerContext context)
        {
            _context = context;
        }

        // GET: IssueType
        public async Task<IActionResult> Index()
        {
            return View(await _context.IssueType.ToListAsync());
        }

        // GET: IssueType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueType = await _context.IssueType
                .SingleOrDefaultAsync(m => m.IssueTypeID == id);
            if (issueType == null)
            {
                return NotFound();
            }

            return View(issueType);
        }

        // GET: IssueType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IssueType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IssueTypeID,Name")] IssueType issueType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issueType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(issueType);
        }

        // GET: IssueType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueType = await _context.IssueType.SingleOrDefaultAsync(m => m.IssueTypeID == id);
            if (issueType == null)
            {
                return NotFound();
            }
            return View(issueType);
        }

        // POST: IssueType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IssueTypeID,Name")] IssueType issueType)
        {
            if (id != issueType.IssueTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issueType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueTypeExists(issueType.IssueTypeID))
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
            return View(issueType);
        }

        // GET: IssueType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueType = await _context.IssueType
                .SingleOrDefaultAsync(m => m.IssueTypeID == id);
            if (issueType == null)
            {
                return NotFound();
            }

            return View(issueType);
        }

        // POST: IssueType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issueType = await _context.IssueType.SingleOrDefaultAsync(m => m.IssueTypeID == id);
            _context.IssueType.Remove(issueType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueTypeExists(int id)
        {
            return _context.IssueType.Any(e => e.IssueTypeID == id);
        }
    }
}
