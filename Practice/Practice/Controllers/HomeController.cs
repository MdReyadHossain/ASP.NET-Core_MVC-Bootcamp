using Microsoft.AspNetCore.Mvc;
using Practice.Models;
using Practice.Models.Repository;

namespace Practice.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var cars = VehicleRepo.GetAllVehicles();
            return View(cars);
        }

        public IActionResult Vehicle(int? id, string? name)
        {
            /* if(id.HasValue) 
                 return new ContentResult { Content = id.ToString() };
             else 
                 return new ContentResult { Content = "invalid id!" };*/

            var vehicle = new Vehicle
            {
                Id = id ?? 0,
                Name = name ?? string.Empty,
            };

            return View(vehicle);
        }
    }
}
