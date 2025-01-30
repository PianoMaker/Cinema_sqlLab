using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema4.Data;
using Cinema4.Models;

namespace Cinema4.Pages.Studio
{
    public class EditModel : PageModel
    {
        private readonly Cinema4.Data.Cinema4Context _context;

        public EditModel(Cinema4.Data.Cinema4Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Studio Studio { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studio =  await _context.Studios.FirstOrDefaultAsync(m => m.Id == id);
            if (studio == null)
            {
                return NotFound();
            }
            Studio = studio;
           ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Studio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudioExists(Studio.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StudioExists(int id)
        {
            return _context.Studios.Any(e => e.Id == id);
        }
    }
}
