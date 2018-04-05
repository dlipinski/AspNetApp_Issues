using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace IssueManager.Models
{
    public class AdminIssueTypeView
    {
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

        public List<IssueType> IssueTypes { get; set; }

    }
}