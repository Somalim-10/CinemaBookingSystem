using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaBookingSystem.Models
{
    public class Showtime
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Starttidspunkt er påkrævet")]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Pris er påkrævet")]
        [Range(1, 1000, ErrorMessage = "Prisen skal være mellem 1 og 1000")]
        [Precision(8, 2)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Du skal vælge en film")]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
