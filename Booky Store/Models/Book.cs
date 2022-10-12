using System.ComponentModel.DataAnnotations;

namespace Booky_Store.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Display(Name="Book Name"),MaxLength(100)]
        public string Name { get; set; }
        [Display(Name = "Author Name"), MaxLength(100)]
        public string  AuthorName { get; set; }
        [Display(Name="Book Image")]
        public byte[] BookImage { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
