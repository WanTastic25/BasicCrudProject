using BasicCrudProject.Data;
using BasicCrudProject.Models;
using BasicCrudProject.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicCrudProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var users = await dbContext.Users.ToListAsync();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(Guid userId)
        {
            var user = await dbContext.Users.FindAsync(userId);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(AddUserViewModel addUserViewModel)
        {
            var user = new User
            {
                Name = addUserViewModel.Name,
                Email = addUserViewModel.Email,
                Role = addUserViewModel.Role,
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("UserList", "Users");
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(User editedUser)
        {
            var user = await dbContext.Users.FindAsync(editedUser.Id);

            if (user is not null)
            {
                user.Name = editedUser.Name;
                user.Email = editedUser.Email;
                user.Role = editedUser.Role;
                user.Verified = editedUser.Verified;

                await dbContext.SaveChangesAsync();
            }
            
            return RedirectToAction("UserList", "Users");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            var user = await dbContext.Users.FindAsync(Id);

            if (user is not null)
            {
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
            }

            return RedirectToAction("UserList", "Users");
        }
    }
}
