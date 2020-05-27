using LibraryProject.Domain.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class AdminUserController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public AdminUserController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = userManager.Users;
            return View(users);
        }

        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel addUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addUserViewModel);
            }

            var user = new AppUser()
            {
                UserName = addUserViewModel.UserName,
                Email = addUserViewModel.Email,
                Birthdate = addUserViewModel.Birthdate,
                City = addUserViewModel.City,
                Country = addUserViewModel.Country
            };
            IdentityResult result = await userManager.CreateAsync(user, addUserViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
           foreach(IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(addUserViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Something went wrong while deleting this user.");
            }
            else
            {
                ModelState.AddModelError("", "This user can't be found");
            }
            return View("Index", userManager.Users);
        }
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var editUser = new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Birthdate = user.Birthdate,
                City = user.City,
                Country = user.Country
            };

            return View(editUser);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel)
        {
            var user = await userManager.FindByIdAsync(editUserViewModel.Id);

            if (user != null)
            {
                user.Email = editUserViewModel.Email;
                user.UserName = editUserViewModel.UserName;
                user.Birthdate = editUserViewModel.Birthdate;
                user.City = editUserViewModel.City;
                user.Country = editUserViewModel.Country;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "User not updated, something went wrong.");
                return View(editUserViewModel);
            }

            return RedirectToAction("Index");
        }
    }
}
