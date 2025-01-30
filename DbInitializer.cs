using Cinema4.Data;
using Cinema4.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema4
{
    internal class DbInitializer
    {
        internal static void Initialize(Cinema4Context context)
        {

            /*застосовувати тільки якщо теба очистити БД і створити з нуля*/
            
            context.Database.EnsureDeleted();            
            context.Database.EnsureCreated();
            /**/


            if (context.Cities.Any()) { return; }

            InitializeCity(context, "Київ", "Україна");
            InitializeCity(context, "Львів", "Україна");
            InitializeCity(context, "Дніпро", "Україна");
            InitializeCity(context, "Харків", "Україна");
            InitializeCity(context, "Одеса", "Україна");

            context.SaveChanges(); //інакше потім проблеми з кінотеатрами

            InitializeGenre(context, "comedy");
            InitializeGenre(context, "drama");
            InitializeGenre(context, "cartoon"); 
            InitializeGenre(context, "science fiction");
            InitializeGenre(context, "documentary");

            InitializeStudio(context, "імені Довженка");
            InitializeStudio(context, "Одеська кіностудія");
            InitializeStudio(context, "Київнаукфільм");
            InitializeStudio(context, "Укранімафільм");
            InitializeStudio(context, "Films.ua");


            InitializeFilm(context, "Тіні забутих предків");
            InitializeFilm(context, "Земля");
            InitializeFilm(context, "Кіборги");
            InitializeFilm(context, "Поводир");
            InitializeFilm(context, "Мої думки тихі");
            InitializeFilm(context, "Вавилон XX");
            InitializeFilm(context, "Енеїда");            
            InitializeFilm(context, "Як козаки...");
            InitializeFilm(context, "Капітошка");
            InitializeFilm(context, "Петрик П'яточкин");

            InitializeCinema(context, "Кіото");
            InitializeCinema(context, "Братислава");
            InitializeCinema(context, "Флоренція");
            InitializeCinema(context, "імені Довженка");
            InitializeCinema(context, "Кінопалац");


            context.SaveChanges();




        }

        private static void InitializeCity(Cinema4Context context, string city, string country)
        {
            City c = new City()
            { Name = city, Country = country };
            context.Add(c);
        }


        private static void InitializeGenre(Cinema4Context context, string name)
        {
            Genre f = new Genre()
            { Name = name};
            context.Add(f);            
        }

        private static void InitializeStudio(Cinema4Context context, string name)
        {
            Studio f = new Studio()
            { Name = name
            
            };
            context.Add(f);
        }

        private static void InitializeFilm(Cinema4Context context, string name)
        {
            
            Film f = new Film()
            {
                Name = name,               

            };
            context.Add(f);
        }

        private static void InitializeCinema(Cinema4Context context, string name)
        {
            var rnd = new Random();
            var city = context.Cities.FirstOrDefault(x => x.Name == "Київ");
            if (city == null)
            {                
                city = new City { Name = "Київ", 
                Country = "Україна"
                };
                context.Cities.Add(city);
                context.SaveChanges();
            }

            Cinema f = new Cinema()
            {
                Name = name,
                CityID = city.Id,
                Seats = rnd.Next(50, 400) // насправді не знаю скільки там місць :)
            };
            context.Add(f);
        }
    }
}