using CrudApp.DTOs;
using CrudApp.Models;
using CrudApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Numerics;

namespace CrudApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly AppDbContext dbContext;

        public UserController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
