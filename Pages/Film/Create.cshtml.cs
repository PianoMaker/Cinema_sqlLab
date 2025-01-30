using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cinema4.Data;
using Cinema4.Models;

namespace Cinema4.Pages.Film
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
        ViewData["DirectorID"] = new SelectList(_context.Directors, "Id", "Name");
        ViewData["GenreID"] = new SelectList(_context.Set<Models.Genre>(), "Id", "Name");
        ViewData["StudioID"] = new SelectList(_context.Set<Models.Studio>(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Models.Film Film { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Films.Add(Film);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
