using System;
using System.ComponentModel.DataAnnotations;
namespace IssueManager.Models
{
    public class Config
    {
        public int ConfigID { get; set; }

        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Value")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Required]
        public string Value { get; set; }

    }
}