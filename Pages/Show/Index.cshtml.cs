using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cinema4.Data;
using Cinema4.Models;
using Microsoft.Data.SqlClient;

namespace Cinema4.Pages.Show
{
    public class IndexModel : PageModel
    {
        private readonly Cinema4.Data.Cinema4Context _context;

        public IndexModel(Cinema4.Data.Cinema4Context context)
        {
            _context = context;
        }

        public IList<Models.Show> Shows { get;set; } = default!;

        public string SortOrder { get; set; }
        public string DateSortParam { get; set; }
        public string FilmSortParam { get; set; }
        public string CinemaSortParam { get; set; }
        public string SeatsSortParam { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {

            DateSortParam = sortOrder == "Date" ? "date_desc" : "Date";
            FilmSortParam = sortOrder == "Films" ? "film_desc" : "Films";
            CinemaSortParam = sortOrder == "Cinemas" ? "cinema_desc" : "Cinemas";
            SeatsSortParam = sortOrder == "Seats" ? "seats_desc" : "Seats";

            var shows = from s in _context.Shows
                    .Include(s => s.Film)
                    .Include(s => s.Cinema)
                        select s;

            shows = sortOrder switch
            {
                "Date" => shows.OrderBy(s => s.DateTime),
                "date_desc" => shows.OrderByDescending(s => s.DateTime),
                "Films" => shows.OrderBy(s => s.Film.Name),
                "film_desc" => shows.OrderByDescending(s => s.Film.Name),
                "Cinemas" => shows.OrderBy(s => s.Cinema.Name),
                "cinema_desc" => shows.OrderByDescending(s => s.Cinema.Name),
                "Seats" => shows.OrderBy(s => s.SeatsLeft),
                "seats_desc" => shows.OrderByDescending(s => s.SeatsLeft),
                _ => shows.OrderBy(s => s.DateTime),
            };
            Shows = await shows.AsNoTracking().ToListAsync();
        }
    }
}
