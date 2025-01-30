﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cinema4.Data;
using Cinema4.Models;

namespace Cinema4.Pages.Cinema
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
        ViewData["CityID"] = new SelectList(_context.Set<Models.City>(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Models.Cinema Cinema { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cinemas.Add(Cinema);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
