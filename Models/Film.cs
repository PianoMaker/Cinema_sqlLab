namespace Cinema4.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? DirectorID { get; set; }

        public Director? Director { get; set; }
        
        public int? GenreID { get; set; }
        public Genre? Genre { get; set; }

        public int? StudioID { get; set; }

        public Studio? Studio { get; set; }
        public int? Year { get; set; }
    }
}
