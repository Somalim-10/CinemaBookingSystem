using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CinemaBookingSystem.Data;
using CinemaBookingSystem.Models;

namespace CinemaBookingSystem.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly CinemaBookingSystem.Data.ApplicationDbContext _context;

        public CreateModel(CinemaBookingSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public string? NewProfileImageUrl { get; set; } // optional image input

        public string ProfilePictureMessage { get; set; } = "";
        public bool ProfilePictureSuccess { get; set; } = false;

        public IActionResult OnGet()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToPage("/AccessDenied"); // Eller til en anden side
            }

            return Page();
        }


        [BindProperty]
        public Movie Movie { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrWhiteSpace(NewProfileImageUrl))
            {
                if (!await UrlExistsAsync(NewProfileImageUrl))
                {
                    ProfilePictureMessage = "The image URL seems to be broken or inaccessible.";
                    ProfilePictureSuccess = false;
                    return Page();
                }

                Movie.ImageUrl = NewProfileImageUrl; // good to go
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }


            _context.Movies.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        private async Task<bool> UrlExistsAsync(string url)
        {
            try
            {
                using var httpClient = new HttpClient();
                using var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));

                return response.IsSuccessStatusCode &&
                       response.Content.Headers.ContentType?.MediaType?.StartsWith("image") == true;
            }
            catch
            {
                return false; // anything goes wrong = assume it's bad
            }
        }
    }
}
