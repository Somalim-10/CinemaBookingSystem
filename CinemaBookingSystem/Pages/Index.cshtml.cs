using CinemaBookingSystem.Data;
using CinemaBookingSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public IndexModel(ApplicationDbContext context) => _context = context;

    public List<Movie> Movies { get; set; } = new();

    public void OnGet()
    {
        Movies = _context.Movies.Take(3).ToList();
    }
}
