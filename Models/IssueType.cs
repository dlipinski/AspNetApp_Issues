using System;
using System.ComponentModel.DataAnnotations;
namespace IssueManager.Models
{
    public class IssueType
    {
        public int IssueTypeID { get; set; }

        public int isActive { get; set; }
        
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

    }
}