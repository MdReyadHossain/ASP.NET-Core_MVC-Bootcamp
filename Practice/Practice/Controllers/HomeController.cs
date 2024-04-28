using Microsoft.AspNetCore.Mvc;
using Practice.Models;
using Practice.Models.Repository;
using System.Reflection.PortableExecutable;

namespace Practice.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var cars = VehicleRepo.GetAllVehicles();
            return View(cars);
        }

        /*
        [FromQuery] - Gets values from the query string.
        [FromRoute] - Gets values from route data.
        [FromForm] - Gets values from posted form fields.
        [FromBody] - Gets values from the request body.
        [FromHeader] - Gets values from HTTP headers.

        if not specify model biniding parameter, it would be work as any of them
        */
        public IActionResult Vehicle([FromQuery]int? id, string? name)
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
