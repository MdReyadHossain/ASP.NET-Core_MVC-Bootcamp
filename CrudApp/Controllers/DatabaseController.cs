using CrudApp.DTOs;
using CrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using CrudApp.Constant;


namespace CrudApp.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly IConfiguration _configuration;

        public DatabaseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DatabaseDTO database)
        {
            if (ModelState.IsValid)
            {
                database.Name = database.Name.Replace(" ", "_");
                string connectionString = AuthConstant.DEFAULT_CONNECTION_STRING;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = $"CREATE DATABASE {database.Name}";
                    SqlCommand cmd = new SqlCommand(query, con);
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        TempData["message"] = "Database Created Successfully";

                        _configuration["ConnectionStrings:UserPortal"] = AuthConstant.GET_CONNECTION_STRING(database.Name);
                    }
                    catch (SqlException e)
                    {
                        TempData["error"] = $"Error Generated. Details: {e.Message}";
                    }
                }

                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(AuthConstant.GET_CONNECTION_STRING(database.Name));

                using (var context = new AppDbContext(optionsBuilder.Options))
                {
                    context.Database.Migrate();
                }

                return RedirectToAction(nameof(Create));
            }

            return View(database);
        }

        [HttpPost]
        public IActionResult DeleteDatabase(DatabaseDTO database)
        {
            database.Name = database.Name.Replace(" ", "_");
            string connectionString = AuthConstant.DEFAULT_CONNECTION_STRING;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"ALTER DATABASE copa MODIFY NAME = {database.Name}";
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    TempData["success"] = "Database Updated Successfully";
                }
                catch (SqlException e)
                {
                    TempData["error"] = $"Error Generated. Details: {e.Message}";
                }

                return RedirectToAction("Index", "Home");
            }
        }

        private void UpdateAppSettings(string newConnectionString)
        {
            // Get the path of the appsettings.json file
            var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            // Read the existing appsettings.json file
            var json = System.IO.File.ReadAllText(appSettingsPath);
            var jsonObj = JObject.Parse(json);

            _configuration["ConnectionStrings:UserPortal"] = newConnectionString;

            /*// Update the UserPortal connection string
            jsonObj["ConnectionStrings"]["UserPortal"] = newConnectionString;

            // Write the updated JSON back to the appsettings.json file
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(appSettingsPath, output);*/
        }
    }
}
