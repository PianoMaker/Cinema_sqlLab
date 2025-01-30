using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema4.Models;
using Microsoft.CodeAnalysis;

namespace Cinema4.Pages
{
    public class IndexModel : PageModel
    {

        private readonly Cinema4.Data.Cinema4Context _context;

        [BindProperty]
        public Models.Show Show { get; set; }



        public IndexModel(Cinema4.Data.Cinema4Context context)
        {
            _context = context;
        }

        public void OnGet()
        {
            LoadViewData(); // Завантажуємо дані при первинному запиті
        }
        public async Task<IActionResult> OnPostAsync(string formId)
        {
            if (formId == "filter")
            {
                LoadViewData(); 
                int? selectedCinemaId = Show.CinemaID; 
                int? selectedFilmId = Show.FilmID;

                var shows = await _context.Shows
                    .Where(s => (!selectedCinemaId.HasValue || s.CinemaID == selectedCinemaId) &&
                                (!selectedFilmId.HasValue || s.FilmID == selectedFilmId))
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = $"{s.DateTime.ToString("g")}  '{s.Film.Name}', кінотеатр {s.Cinema.Name}"
                    })
                    .ToListAsync();

                ViewData["ShowList"] = shows; 

                return Page();
            }
            else if (formId == "select")
            {
                return RedirectToPage("/Buy", new { id = Show.Id });
            }
            else return Page();
        }

        private void LoadViewData()
        {
            var cinemas = _context.Cinemas.ToList();
            var cinemaList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Обрати кінотеатр" } // Порожня опція
            };
            cinemaList.AddRange(cinemas.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }));
            ViewData["CinemaID"] = cinemaList;

            var films = _context.Films.ToList();
            var filmList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Обрати фільм" } // Порожня опція
            };
            filmList.AddRange(films.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }));
            ViewData["FilmID"] = filmList;

            // Завантажуємо покази для первинного запиту
            var shows = _context.Shows.ToList();
            var showList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Обрати сеанс" } // Порожня опція
            };
            showList.AddRange(shows.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"{s.DateTime.ToString("g")}  '{s.Film.Name}', кінотеатр {s.Cinema.Name}"
            }));

            ViewData["ShowList"] = showList;
        }
    }
}
