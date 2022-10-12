using System.ComponentModel.DataAnnotations;

namespace Booky_Store.Models
{
	public class AddBook
	{
        public int Id { get; set; }
        [Display(Name = "Book Name"), MaxLength(100)]
        public string Name { get; set; }
        [Display(Name = "Author Name"), MaxLength(100)]
        public string AuthorName { get; set; }
        public byte[]? BookImage { get; set; }
    }
}
