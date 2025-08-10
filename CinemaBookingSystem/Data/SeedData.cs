using CinemaBookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using static CinemaBookingSystem.Models.User;

namespace CinemaBookingSystem.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Username = "Goku", Password = "123", Role = UserRole.Admin },
                    new User { Username = "Vegeta", Password = "123", Role = UserRole.Admin },
                    new User { Username = "Gohan", Password = "123", Role = UserRole.Kunde },
                    new User { Username = "Piccolo", Password = "123", Role = UserRole.Kunde },
                    new User { Username = "Krillin", Password = "123", Role = UserRole.Kunde },
                    new User { Username = "Bulma", Password = "123", Role = UserRole.Kunde },
                    new User { Username = "Trunks", Password = "123", Role = UserRole.Kunde }
                );
                context.SaveChanges();
            }

            if (!context.Movies.Any())
            {
                context.Movies.AddRange(
                    new Movie { Title = "Dragon Ball Super: Broly", Genre = "Action", Description = "Saiyans vs Broly", DurationMinutes = 100, ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTA5MTc1M2EtZWQ2Ni00ZmU2LTg3MzQtOTliMjE4OGM0ZWFiXkEyXkFqcGc@._V1_.jpg" },
                    new Movie { Title = "Dragon Ball Z: Resurrection 'F'", Genre = "Action", Description = "Frieza returns", DurationMinutes = 93, ImageUrl = "https://m.media-amazon.com/images/I/81zI6wDMzCL._UF1000,1000_QL80_.jpg" }
                );
                context.SaveChanges();
            }

            if (!context.Showtimes.Any())
            {
                var broly = context.Movies.FirstOrDefault(m => m.Title == "Dragon Ball Super: Broly");
                var frieza = context.Movies.FirstOrDefault(m => m.Title == "Dragon Ball Z: Resurrection 'F'");

                if (broly != null && frieza != null)
                {
                    context.Showtimes.AddRange(
                        new Showtime { StartTime = DateTime.Today.AddHours(18), Price = 89, MovieId = broly.Id },
                        new Showtime { StartTime = DateTime.Today.AddDays(1).AddHours(20), Price = 95, MovieId = frieza.Id }
                    );
                    context.SaveChanges();
                }
            }

            if (!context.Bookings.Any())
            {
                var showtime1 = context.Showtimes.Include(s => s.Movie).FirstOrDefault();
                if (showtime1 != null)
                {
                    context.Bookings.AddRange(
                        new Booking { Seats = 2, ShowTimeId = showtime1.Id, UserName = "Gohan" },
                        new Booking { Seats = 4, ShowTimeId = showtime1.Id, UserName = "Piccolo" }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
