namespace Practice.Models.Repository
{
    public static class VehicleRepo
    {
        public static List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> cars = [
                new Vehicle { Id = 1, Name = "BMW", Price = 120 }, 
                new Vehicle { Id = 1, Name = "Pagani", Price = 555 }
            ];
            return cars;
        }
    }
}
