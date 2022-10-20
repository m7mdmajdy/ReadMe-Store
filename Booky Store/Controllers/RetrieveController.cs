using Booky_Store.Data;
using Booky_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Security.Principal;
using Volo.Abp.Users;

namespace Booky_Store.Controllers
{
    public class RetrieveController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RetrieveController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            IEnumerable<Book> AllBooks=_context.books.Include(m=>m.ApplicationUser).ToList();
            return View(AllBooks);
        }
        public IActionResult Add()
        {
            //To get Current user Id
            var claimsIdentity = (ClaimsIdentity)User.Identity;

            var user = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null) return NotFound("No user Logged in");

            var userId = user.Value;
            //To get current user usind Id
            ApplicationUser MyUser = _context.Users.Find(userId);
            if (MyUser == null) return NotFound("No User found");

            ApplicationUserBook CurrUser=new ApplicationUserBook();
            CurrUser.ApplicationUser = new ApplicationUser
            {
                UserName=MyUser.UserName,
                Email=MyUser.Email,
                FirstName=MyUser.FirstName,
                LastName=MyUser.LastName,
                PhoneNumber=MyUser.PhoneNumber,
            };

            return View(CurrUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ApplicationUserBook AddedBook)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;

            var user = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null) return NotFound("No user Logged in");

            var userId = user.Value;
            ApplicationUser MyUser = _context.Users.Find(userId);
            if (MyUser == null) return NotFound("No User found");
            

            Book book;
            book = new Book
            {
                ApplicationUser=MyUser,
                AuthorName=AddedBook.Book.AuthorName,
                BookImage=AddedBook.Book.BookImage,
                Name=AddedBook.Book.Name,
            };
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();
                using (var datastream = new MemoryStream())
                {
                    await file.CopyToAsync(datastream);
                    book.BookImage=datastream.ToArray();
                }
            }
            if(book.Name is null || book.AuthorName is null || book.BookImage is null)
            {
                return BadRequest("Wrong input");
            }
            _context.books.Add(book);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public  IActionResult BookDetails(int id)
        {
            Book RetrivedBook =  _context.books.Include(m=>m.ApplicationUser).FirstOrDefault(x=>x.Id==id);

            if (RetrivedBook == null) return NotFound();
            return View(RetrivedBook);
        }
        public IActionResult RemoveBook(int id)
        {
            var book = _context.books.FirstOrDefault(x=>x.Id==id);
            if(book==null) return NotFound();

            _context.books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}



//GET
//var claimsIdentity = (ClaimsIdentity)User.Identity;
//var user = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
//var userId = user.Value;
//ApplicationUser MyUser = _context.Users.Find(userId);

//ApplicationUserBook AppUser = new ApplicationUserBook();
//AppUser.ApplicationUser = new ApplicationUser() { 
//    UserName=MyUser.UserName,
//    FirstName=MyUser.FirstName,
//    LastName=MyUser.LastName,
//    PhoneNumber=MyUser.PhoneNumber,
//};




//POST
//if (!User.Identity.IsAuthenticated)
//{
//    return View(AddedBook);
//}
//if (User.Identity.IsAuthenticated)
//{
//    var claimsIdentity = (ClaimsIdentity)User.Identity;
//    var user = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
//    var userId = user.Value;
//    ApplicationUser MyUser = _context.Users.Find(user.Value);
//    Book book = new Book()
//    {
//        Id = AddedBook.Book.Id,
//        AuthorName = AddedBook.Book.AuthorName,
//        Name = AddedBook.Book.Name,
//        PhoneNumber = AddedBook.ApplicationUser.PhoneNumber,
//        ApplicationUser = new ApplicationUser
//        {
//            Email=MyUser.Email,
//            FirstName=MyUser.FirstName,
//            LastName=MyUser.LastName,
//            EmailConfirmed=MyUser.EmailConfirmed,
//            Books = MyUser.Books,
//            Id = MyUser.Id 
//        },
//        ApplicationUserId = MyUser.Id
//    };
//    if (!ModelState.IsValid)
//    {
//        return NotFound("Wrong input");
//    }
//    _context.books.Add(book);
//    _context.SaveChanges();
//    return RedirectToAction("Index");
//}