using CrudApp.Models;
using CrudApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly AppDbContext dbContext;

        public AdminController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var users = await dbContext.Users
                                .OrderByDescending(user => user.Id)
                                .Where(user => user.Status == true && user.Type != "admin")
                                .ToListAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> PendingList()
        {
            var users = await dbContext.Users
                                .OrderByDescending(user => user.Id)
                                .Where(user => user.Status == false && user.Type != "admin")
                                .ToListAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(Guid uid)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Uid == uid);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(User user)
        {
            var updateUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Uid == user.Uid);
            if (updateUser != null)
            {
                var existedUser = (from dbUser in dbContext.Users
                                   where updateUser.Uid != dbUser.Uid &&
                                   (user.UserName == dbUser.UserName || user.Email == dbUser.Email)
                                   select dbUser).SingleOrDefault();

                ModelState.Remove("Phone");
                ModelState.Remove("Password");
                if (!ModelState.IsValid)
                {
                    ViewBag.ErrorMsg = "Please fill the input field properly!";
                }
                else if (existedUser != null)
                {
                    ViewBag.ErrorMsg = "User has already exist!";
                }
                else if (updateUser != null)
                {
                    updateUser.Name = user.Name;
                    updateUser.Email = user.Email;
                    updateUser.Phone = user.Phone ?? "";
                    updateUser.UserName = user.UserName;
                    dbContext.Users.Update(updateUser);
                    await dbContext.SaveChangesAsync();
                    ViewBag.SuccessMsg = "User Created Succesful!";

                    return RedirectToAction("UserList", "Admin");
                }
                else
                {
                    ViewBag.ErrorMsg = "Password not Matched!";
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AcceptUser(Guid uid)
        {
            /*var users = await dbContext.Users.FindAsync(uid);*/
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Uid == uid);
            if (user != null)
            {
                user.Status = true;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("PendingList", "Admin");

        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(Guid uid, bool isUserList)
        {
            /*var users = await dbContext.Users.FindAsync(uid);*/
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Uid == uid);
            if (user != null)
            {
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();
            }
            if (isUserList)
                return RedirectToAction("UserList", "Admin");
            else
                return RedirectToAction("PendingList", "Admin");
        }
    }
}
