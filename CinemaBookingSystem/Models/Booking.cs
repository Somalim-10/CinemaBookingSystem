using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaBookingSystem.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Antal sæder er påkrævet.")]
        public int Seats { get; set; }

        [Required(ErrorMessage = "Du skal vælge en visning.")]
        public int ShowTimeId { get; set; }

        public Showtime Showtime { get; set; } = null!;

        [Required]
        public string UserName { get; set; } = default!; // 👈 TILFØJ DENNE LINJE
    
}
}
