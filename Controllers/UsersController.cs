using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ProjectManager.ViewModels;
using Microsoft.Data.SqlClient;

namespace ProjectManager.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            public string Name { get; set; }

            [Display(Name = "Role")]
            public string RoleId { get; set; }
        }

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(ApplicationDbContext context, 
            UserManager<Models.User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //GET: Users
        public IActionResult Index()
        {
            var model = _userManager.Users.ToListAsync().Result;
            return View(model);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var roleId = _context.UserRoles.Where(ur => ur.UserId.Equals(id)).FirstOrDefault().RoleId;
            ViewBag.Role = _context.Roles.Where(r => r.Id.Equals(roleId)).FirstOrDefault().Name;

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewBag.Roles = _context.Roles.ToList();
            var model = new InputModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InputModel model)
        {
            var role = _roleManager.FindByIdAsync(model.RoleId).Result;
            if (ModelState.IsValid)
            {
                var user = new ProjectManager.Models.User { UserName = model.Email, Email = model.Email };

                var result = await _userManager.CreateAsync(user, model.Password);
                var result2 = await _userManager.AddToRoleAsync(user, role.Name);
                if (!result.Succeeded || !result2.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Create();
                }       
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UpdateUserViewModel()
            {
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateUserViewModel model)
        {
            var user = _userManager.Users.Where(u => u.Email.Equals(model.Email)).FirstOrDefault();
            if (user == null)
            {
                TempData["Message"] = $"User with email: {user.Email} was not edited.";
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
 
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.Surname = model.Surname;
            user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                {
                TempData["Message"] = $"User with email: {user.Email} was not edited.";
                return View();
                }
            TempData["Message"] = $"User with email: {user.Email} was edited successfully.";
            return View();
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                TempData["ErrorDeleting"] = "User could not be deleted. Delete all projects and tasks User has resposibility for first.";
                return RedirectToAction(nameof(Delete));
            }
        }

        private async Task<List<string>> GetUserRoles(Models.User user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        public async Task<IActionResult> AssignRole(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return NotFound();
            }
            ViewBag.UserName = user.UserName;
            var model = new List<Models.ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new Models.ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }
            ViewBag.Roles = model;
            return View(new Models.ManageUserRolesViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(Models.ManageUserRolesViewModel model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRoleAsync(user, model.RoleName);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("Index");
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

    }
}
