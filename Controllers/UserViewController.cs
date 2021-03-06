using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IssueManager.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IssueManager.Controllers
{
    public class UserViewController : Controller
    {
        private readonly IssueManagerContext _context;


        public UserViewController(IssueManagerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userView = new UserView();
            var definedIssues =
                from m in _context.Issue.Include(m =>m.IssueType)
                where m.IsDefined == 1
                select m;
            
            userView.Issues = await definedIssues.ToListAsync();   
            ViewData["IssueTypeID"] = new SelectList(_context.IssueType.OrderBy(it => it.Name).Where(itia => itia.isActive == 1), "IssueTypeID", "Name", userView.IssueTypeID);         
            return View(userView);
        }

        public async Task<IActionResult> UserCreateNew([Bind("IssueID,IsDefined,Content,Solution,State,Created,Solved,CreatorID,SolverID,IssueTypeID")] Issue issue)
        {
            issue.IsDefined = 0;
            issue.State = 0;
            issue.Solution = "undefined";
            issue.Created = DateTime.Now;
            issue.Solved = DateTime.Now;
            issue.CreatorID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            issue.SolverID = "undefined";
            _context.Add(issue);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        public async Task<IActionResult> MyIssues()
        {
            var myId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var issueUser =
                from issue in _context.Issue.Include(i => i.IssueType)
                join  user in _context.ApplicationUser
                on issue.SolverID equals user.Id
                into iu
                from issueuser in iu.DefaultIfEmpty()
                where issue.CreatorID == myId && issue.IsDefined == 0
                orderby issue.Created descending
                select new IssueUser(
                    issueuser.UserName,
                    issue
                );  

            return View(await issueUser.ToListAsync());
        }
    }
}