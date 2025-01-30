namespace Cinema4.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int? CityID { get; set; } 
        public City? City { get; set; }
        public string? Address { get; set; }
        public int Seats { get; set; }
    }
}
