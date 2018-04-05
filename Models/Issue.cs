using System;
using System.ComponentModel.DataAnnotations;
using core_sec;
using core_sec.Models;
namespace IssueManager.Models

{
    public class Issue
    {
        public int IssueID { get; set; }

        [Display(Name = "IsDefined")]
        [Required]
        public int IsDefined { get; set; }

        [Display(Name = "Content")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        [Required]
        public string Content { get; set; }

        [Display(Name = "Solution")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Solution { get; set; }

        [Display(Name = "State")]
        [Required]
        public int State { get; set; }
        
        [Display(Name = "Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString = "{0:H:mm dd/MM}")]
        [Required]
        public DateTime Created { get; set; }

        [Display(Name = "Solved")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime Solved { get; set; }

        [Display(Name = "CreatorID")]
        [StringLength(100)]
        [Required]
        public string CreatorID { get; set; }

        [Display(Name = "SolverID")]
        [StringLength(100)]
        public string SolverID { get; set; }

        [Display(Name = "Issue Type")]
        [Required]
        public int IssueTypeID { get; set; }

        [Display(Name = "Issue Type")] 
        [Required]
        public IssueType IssueType { get; set; }
    }
}