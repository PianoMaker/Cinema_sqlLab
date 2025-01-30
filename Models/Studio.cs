using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema4.Models
{
    public class Studio
    {
        public int Id { get; set; }
        public string Name { get; set; }
                
        public int? CityId {  get; set; }

        public City? City { get; set; }
    }
}
