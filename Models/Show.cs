namespace Cinema4.Models
{
    public class Show
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public int FilmID { get; set; }

        public Film? Film { get; set; }

        public int CinemaID { get; set; }
        public Cinema? Cinema { get; set; }
        
        public int SeatsLeft { get; set; }

        public Show()
        {            
            SeatsLeft = 0;
        }

        public void Initialize(Cinema? cinema)
        {
            Cinema = cinema;
            SeatsLeft = Cinema?.Seats ?? 0; 
        }

    }

    
}
