using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace IssueManager.Models
{
    public class MovieArtistViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Movie> Movies;
    }
}