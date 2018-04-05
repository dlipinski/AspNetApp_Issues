using System;
using System.ComponentModel.DataAnnotations;
namespace IssueManager.Models
{
    public class IssueUser
    {
        

        public IssueUser(string nameSurname, Issue issue)
        {
            NameSurname = nameSurname;
            Issue = issue;
        }

        [Display(Name = "Name Surname")]
        [StringLength(3, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Required]
        public String NameSurname { get; set; }

        public Issue Issue { get; set; }

    }
}