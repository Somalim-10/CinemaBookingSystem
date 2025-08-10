using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CinemaBookingSystem.Data;
using CinemaBookingSystem.Models;

namespace CinemaBookingSystem.Pages.ShowTimes
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // 👇 BindProperty gør at Razor-formen kan læse og skrive til denne
        [BindProperty]
        public Showtime Showtime { get; set; } = new Showtime(); // 👈 Vi initialiserer den for at undgå null-fejl


        // 👇 Dropdown med film
        public SelectList MovieOptions { get; set; } = default!;

        // Når siden loades første gang (GET-request)
        public IActionResult OnGet()
        {
            Showtime = new Showtime
            {
                StartTime = DateTime.Now // sæt nuværende tid så feltet ikke starter som år 0001
            };
            // 🔁 Genskaber dropdown med alle film (vises i select)
            MovieOptions = new SelectList(_context.Movies, "Id", "Title");

            return Page(); // Viser selve siden
        }

        // Når brugeren trykker "Gem" (POST-request)
        public async Task<IActionResult> OnPostAsync()
        {
            MovieOptions = new SelectList(_context.Movies, "Id", "Title");
            ModelState.Remove("Showtime.Movie");
            string date = Request.Form["date"];
            string time = Request.Form["time"];

            if (DateTime.TryParse($"{date} {time}", out DateTime parsedDateTime))
            {
                Showtime.StartTime = parsedDateTime;
            }
            else
            {
                ModelState.AddModelError("Showtime.StartTime", "Dato og tid er ugyldig.");
            }
            if (Showtime.StartTime < DateTime.Now)
            {
                ModelState.AddModelError("Showtime.StartTime", "Starttid må ikke være i fortiden.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"❌ FEJL I: {entry.Key} – {error.ErrorMessage}");
                    }
                }

                TempData["Fejl"] = "Noget gik galt – tjek felterne igen.";
                return Page();
            }

            _context.Showtimes.Add(Showtime);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

    }
}
