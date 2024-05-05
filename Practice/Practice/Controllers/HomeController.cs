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
        public IActionResult Vehicle([FromRoute] int? id, string? name)
        {
            /* 
            if(id.HasValue) 
                return new ContentResult { Content = id.ToString() };
            else 
                return new ContentResult { Content = "invalid id!" };
            */

            var vehicle = new Vehicle
            {
                Id = id ?? 0,
                Name = name ?? string.Empty,
            };

            return View(vehicle);
        }

        public IActionResult Create()
        {
            ViewBag.Action = "Create";
            return View();
        }

        [HttpPost]
        public IActionResult Create(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                VehicleRepo.CreateVehicle(vehicle);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Action = "Create";
            return View(vehicle);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";

            var vechicle = VehicleRepo.GetVehicle(id);
            return View(vechicle);
        }

        [HttpPost]
        public IActionResult Edit(Vehicle vehicle)
        {
            if(ModelState.IsValid)
            {
                VehicleRepo.UpdateVehicle(vehicle.Id, vehicle);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Action = "Edit";
            return View(vehicle);
        }

        public IActionResult Delete(int id)
        {
            VehicleRepo.DeleteVehicle(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
