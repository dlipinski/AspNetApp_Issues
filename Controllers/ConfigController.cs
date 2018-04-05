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
    public class ConfigController : Controller
    {
        private readonly IssueManagerContext _context;

        public ConfigController(IssueManagerContext context)
        {
            _context = context;
        }

        // GET: Config
        public async Task<IActionResult> Index()
        {
            return View(await _context.Config.ToListAsync());
        }

        // GET: Config/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var config = await _context.Config
                .SingleOrDefaultAsync(m => m.ConfigID == id);
            if (config == null)
            {
                return NotFound();
            }

            return View(config);
        }

        // GET: Config/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Config/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConfigID,Name,Value")] Config config)
        {
            if (ModelState.IsValid)
            {
                _context.Add(config);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(config);
        }

        // GET: Config/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var config = await _context.Config.SingleOrDefaultAsync(m => m.ConfigID == id);
            if (config == null)
            {
                return NotFound();
            }
            return View(config);
        }

        // POST: Config/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConfigID,Name,Value")] Config config)
        {
            if (id != config.ConfigID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(config);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigExists(config.ConfigID))
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
            return View(config);
        }

        // GET: Config/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var config = await _context.Config
                .SingleOrDefaultAsync(m => m.ConfigID == id);
            if (config == null)
            {
                return NotFound();
            }

            return View(config);
        }

        // POST: Config/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var config = await _context.Config.SingleOrDefaultAsync(m => m.ConfigID == id);
            _context.Config.Remove(config);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfigExists(int id)
        {
            return _context.Config.Any(e => e.ConfigID == id);
        }
    }
}
