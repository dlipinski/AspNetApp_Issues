using System;
using System.ComponentModel.DataAnnotations;
namespace IssueManager.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [Display(Name = "Tytuł")] //p1.2
        [StringLength(60, MinimumLength = 3)] //p1.1
        [Required]
        public string Title { get; set; }

        [Display(Name = "Data wydania")] //p1.2
        [DataType(DataType.Date)] //p1.1
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] //p1.2
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Gatunek")] //p1.2
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")] //p1.1
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }

        [Display(Name = "Cena")] //p1.2
        [Range(1, 100)] //p1.1
        [DataType(DataType.Currency)]  //p1.2 
        public decimal Price { get; set; }

        [Display(Name = "Opis")] //p1.2
        public String Description { get; set; }

        [Display(Name = "Reżyser")] //p1.2
        [Required]
        public int ArtistId { get; set; }

        [Display(Name = "Reżyser")] //p1.2
        [Required]
        public Artist Artist { get; set; }

        
    }
}