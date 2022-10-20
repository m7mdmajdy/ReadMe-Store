using System.ComponentModel.DataAnnotations;

namespace Booky_Store.Models.PageViewModels
{
    public class UserRoles
    {
        public string Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }

    }
}
