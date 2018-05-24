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
using core_sec.Models;
using core_sec.Models.AccountViewModels;
using core_sec.Services;

namespace IssueManager.Controllers
{
    public class AdminViewController : Controller
    {
        private readonly IssueManagerContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminViewController(IssueManagerContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var unsolvedIssues = 
                from issue in _context.Issue.Include(m => m.IssueType)
                join user in _context.ApplicationUser
                on issue.CreatorID equals user.Id
                where issue.State == 0
                orderby issue.Created descending
                select new IssueUser(user.UserName,issue);

            return View(await unsolvedIssues.ToListAsync());
        }

        public async Task<IActionResult> GetIssue(int? id){
            
            if (id == null)
            {
                return NotFound();
            }
           
            var issueToSolve = await _context.Issue.SingleOrDefaultAsync(m => m.IssueID == id);
            issueToSolve.State = 1;
            try
                {
                    _context.Update(issueToSolve);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issueToSolve.IssueID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(SolveIssue), new {id = id });
        }
        
        public async Task<IActionResult> SolveIssue (int? id)
        {
            var issue2 = 
                await
                (from issue in _context.Issue.Include(i => i.IssueType)
                where  issue.IssueID== id
                select issue)
                .FirstOrDefaultAsync();

            issue2.State =1;

            try
            {
                _context.Update(issue2);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueExists(issue2.IssueID))
                {
                   return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var issueUser =
                from issue in _context.Issue.Include(i => i.IssueType)
                join user in _context.ApplicationUser
                on issue.CreatorID equals user.Id
                where issue.IssueID == id
                select new IssueUser(user.UserName,issue);  



            return View(await issueUser.FirstOrDefaultAsync());
            
        }

       
        public async Task<IActionResult> SolveIssueF ([FromForm(Name="Issue.IssueID")] int id,[FromForm(Name="Issue.Solution")] String solution)
        {
            var issue = await 
                (from issues in _context.Issue.Include(m => m.IssueType)
                where issues.IssueID == id
                select issues)
                .FirstOrDefaultAsync();
            issue.Solution = solution;
            issue.State = 2;
            issue.Solved = DateTime.Now;
            issue.SolverID =  User.FindFirstValue(ClaimTypes.NameIdentifier);

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
        
        public async Task<IActionResult> ManageIssueTypes()
        {
            AdminIssueTypeView adminIssueTypeView = new AdminIssueTypeView();
            var issueTypes = 
                from issueType in _context.IssueType
                where issueType.isActive == 1
                orderby issueType.Name
                select issueType;

            adminIssueTypeView.IssueTypes = await issueTypes.ToListAsync();
            
            return View(adminIssueTypeView);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIssueType([Bind("IssueTypeID,Name")] IssueType issueType)
        {
            if (ModelState.IsValid)
            {
                var issueTypeOlds = 
                    from issueTypes in _context.IssueType
                    where issueTypes.Name.Trim() == issueType.Name.Trim()
                    select issueTypes;
                    
                var issueTypeOld = await issueTypeOlds.FirstOrDefaultAsync();
                if (issueTypeOld != null)
                {
                    issueTypeOld.isActive = 1;
                    _context.Update(issueTypeOld);
                }
                else
                {
                    issueType.isActive=1;
                    _context.Add(issueType);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageIssueTypes));
            }
            return RedirectToAction(nameof(ManageIssueTypes));
        }
   
        public async Task<IActionResult> DeleteIssueType(int id)
        {
            var issueType = await _context.IssueType.SingleOrDefaultAsync(m => m.IssueTypeID == id);
            issueType.isActive = 0;
            _context.IssueType.Update(issueType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageIssueTypes));
        }

        public async Task<IActionResult> ManageDefinedIssues()
        {
            var userView = new UserView();
            var definedIssues =
                from m in _context.Issue.Include(m =>m.IssueType)
                where m.IsDefined == 1
                select m;
            userView.Issues = await definedIssues.ToListAsync();   
            ViewData["IssueTypeID"] = new SelectList(_context.IssueType, "IssueTypeID", "Name", userView.IssueTypeID);         
            return View(userView);
        }

        public async Task<IActionResult> DeleteDefinedIssue(int id)
        {
            var issue = await _context.Issue.SingleOrDefaultAsync(m => m.IssueID == id);
            _context.Issue.Remove(issue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageDefinedIssues));
        }
        private bool IssueExists(int id)
        {
            return _context.Issue.Any(e => e.IssueID == id);
        }

         public async Task<IActionResult> AdminCreateNewDefinedIssue([Bind("IssueID,IsDefined,Content,Solution,State,Created,Solved,CreatorID,SolverID,IssueTypeID")] Issue issue)
        {
            issue.IsDefined = 1;
            issue.State = 3;
            issue.Solution = issue.Solution;
            issue.Created = DateTime.Now;
            issue.Solved = DateTime.Now;
            issue.CreatorID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            issue.SolverID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Add(issue);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageDefinedIssues));
            
        }

        public async Task<IActionResult> EditDefinedIssue([FromQuery(Name="item.IssueID")] int id, [FromQuery(Name="@item.Content")] String content, [FromQuery(Name="@item.Solution")] String solution)
        {        
            var issueList = 
                from issue in _context.Issue.Include(m => m.IssueType)
                where issue.IssueID == id
                select issue;
            
            Issue editedIssue = await issueList.FirstOrDefaultAsync();

            editedIssue.Content = content;
            editedIssue.Solution = solution;

            _context.Update(editedIssue);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageDefinedIssues));
        }

        public async Task<IActionResult> ManageUsers()
        {
            
           

            var wannaBe =
            _userManager.Users;

            var users = await
                _userManager.GetUsersInRoleAsync("User");

            var admins = await
                _userManager.GetUsersInRoleAsync("Admin");

            var wannaBeList = wannaBe.ToList();

            foreach( var user in users)
            {
                wannaBeList.Remove(user);
            }

            foreach( var admin in admins)
            {
                wannaBeList.Remove(admin);
            }

            AdminUsersView adminUsersView = new AdminUsersView();

            adminUsersView.WannaBe = wannaBeList;
            adminUsersView.Users =  users.ToList();
            adminUsersView.Admins =  admins.ToList();

            return View(adminUsersView);      
        }

       
        public async Task<IActionResult> SetWannaBeUser(String id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            await _userManager.AddToRoleAsync(user,"User");
            return RedirectToAction(nameof(ManageUsers));   
        }
  
         public async Task<IActionResult> DeleteWannaBe(String id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(ManageUsers));   
        }
        public async Task<IActionResult> SetUserAdmin(String id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(user,"User");
            await _userManager.AddToRoleAsync(user,"Admin");
            return RedirectToAction(nameof(ManageUsers));   
        }

        public async Task<IActionResult> DeleteUser(String id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(ManageUsers));     
        }

        public async Task<IActionResult> SetAdminUser(String id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(user,"Admin");
            await _userManager.AddToRoleAsync(user,"User");
            return RedirectToAction(nameof(ManageUsers));   
        }

        public async Task<IActionResult> SolvedIssues()
        {
            var myId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var issueUser =
                from issue in _context.Issue.Include(i => i.IssueType)
                join  user in _context.ApplicationUser
                on issue.SolverID equals user.Id
                into iu
                from issueuser in iu.DefaultIfEmpty()
                where issue.SolverID == myId && issue.IsDefined == 0
                orderby issue.Created descending
                select new IssueUser(
                    issueuser.UserName,
                    issue
                );  

            return View(await issueUser.ToListAsync());
        }
    }
}