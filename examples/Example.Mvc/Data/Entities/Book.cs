using System.ComponentModel.DataAnnotations;

namespace Example.Mvc.Data.Entities
{


    public class Book
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Author { get; set; }
    }
}