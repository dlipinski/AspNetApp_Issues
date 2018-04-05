using System;
using System.ComponentModel.DataAnnotations;
using core_sec;
using core_sec.Models;
using System.Collections.Generic;

namespace IssueManager.Models

{
    public class UserView
    {

        [Display(Name = "Content")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        [Required]
        public string Content { get; set; }

        [Display(Name = "Solution")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Solution { get; set; }

        [Display(Name = "Issue Type")]
        [Required]
        public int IssueTypeID { get; set; }

        [Display(Name = "Issue Type")] 
        [Required]
        public IssueType IssueType { get; set; }


        public List<Issue> Issues { get; set; }
    }
}