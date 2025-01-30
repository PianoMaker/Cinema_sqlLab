using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cinema4.Data;
using Cinema4.Models;

namespace Cinema4.Pages.Show
{
    public class CreateModel : PageModel
    {
        private readonly Cinema4.Data.Cinema4Context _context;

        public CreateModel(Cinema4.Data.Cinema4Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CinemaID"] = new SelectList(_context.Cinemas, "Id", "Name");
            ViewData["FilmID"] = new SelectList(_context.Films, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Models.Show Show { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var cinema = await _context.Cinemas.FindAsync(Show.CinemaID);
            Show.Initialize(cinema);

            _context.Shows.Add(Show);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        public async Task<IActionResult?> OnPostRandomCreateAsync()
        {
            var random = new Random();
            if (!ModelState.IsValid || !_context.Cinemas.Any() || !_context.Films.Any())
                return Page();

            for (int i = 0; i < random.Next(100, 1000); i++)
            {
                var newShow = new Models.Show
                {
                    DateTime = DateTime.Now.AddDays(random.Next(1, 30))
                                .Date 
                                .AddHours(10 + random.Next(0, 12)) 
                                .AddMinutes(0),
                    CinemaID = random.Next(1, _context.Cinemas.Count() + 1),
                    FilmID = random.Next(1, _context.Films.Count() + 1)
                };
                var cinema = _context.Cinemas.FirstOrDefault(c => c.Id == newShow.CinemaID);
                newShow.Initialize(cinema);

                _context.Shows.Add(newShow);
            }
            await _context.SaveChangesAsync();
                        
            return RedirectToPage("./Index");
        }
    }
}
