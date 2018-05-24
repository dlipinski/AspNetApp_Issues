using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IssueManager.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using core_sec.Models;

namespace IssueManager.Models
{
    public class AdminUsersView
    {
        public List<ApplicationUser> WannaBe { get; set; }

        public List<ApplicationUser> Users { get; set; }

        public List<ApplicationUser> Admins { get; set; }
    }
}