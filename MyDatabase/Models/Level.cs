namespace MyDatabase.Models
{
    public class Level
    {
        public int LevelId { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Certificate>? Certificates { get; set;}
        
    }
}