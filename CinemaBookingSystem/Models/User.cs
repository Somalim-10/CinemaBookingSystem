using System.ComponentModel.DataAnnotations;

namespace CinemaBookingSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }
        public enum UserRole
        {
            Kunde = 0,
            Admin = 1
        }
    }
}
