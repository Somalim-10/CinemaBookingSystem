using System.ComponentModel.DataAnnotations;

namespace CinemaBookingSystem.Models
{
    public class Movie
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        [Display(Name = "Duration (minutes)")]
        public int DurationMinutes { get; set; } // Længde i minutter

        [Display(Name = "Poster URL")]
        [Url]
        [RegularExpression(@".*\.(jpg|jpeg|png|gif|bmp|webp)$", ErrorMessage = "URL skal pege på et billede (.jpg, .jpeg, .png, .gif, .bmp, .webp)")]
        public string? ImageUrl { get; set; }

    }
}
