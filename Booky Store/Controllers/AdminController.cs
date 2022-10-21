using Booky_Store.Data;
using Booky_Store.Models;
using Booky_Store.Models.PageViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Booky_Store.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Select(user => new UserRoles
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult()
            }).ToListAsync();

            return View(users);
        }
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();

            var viewModel = new UserRolesViewModel
            {
                Email = user.Email,
                UserId = user.Id,
                UserRoles = roles.Select(role => new RoleViewModel
                {
                    Name = role.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesViewModel userModel)
        {
            var user = await _userManager.FindByIdAsync(userModel.UserId);
            if(user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach(var x in userModel.UserRoles)
            {
                if (userRoles.Any(r => r == x.Name) && !x.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, x.Name);
                if(!userRoles.Any(r=>r == x.Name)&&x.IsSelected)
                    await _userManager.AddToRoleAsync(user,x.Name);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UserInfo(string userId)
        {
            var user=await _context.Users.Include(m=>m.Books).FirstOrDefaultAsync(m=>m.Id==userId);
            if(user==null) return NotFound();
            return View(user);
        }
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user=await _context.Users.FirstOrDefaultAsync(m=>m.Id==userId);
            if (user == null) return NotFound();
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
