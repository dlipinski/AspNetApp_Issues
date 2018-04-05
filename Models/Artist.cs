using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace IssueManager.Models
{
    public class Artist
    {
        public int ID { get; set; }

        [Display(Name = "Imie")] //p1.2
        [StringLength(60, MinimumLength = 3)] //p1.1
        [Required]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")] //p1.2
        [StringLength(60, MinimumLength = 3)] //p1.1
        [Required]
        public string Surname { get; set; }

    }
}