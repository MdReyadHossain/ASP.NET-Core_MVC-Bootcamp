namespace Practice.Models.Repository
{
    public static class VehicleRepo
    {
        private readonly static List<Vehicle> _Vehicles = [
            new Vehicle { Id = 1, Name = "BMW", Price = 120 },
            new Vehicle { Id = 2, Name = "Pagani", Price = 555 }
        ];

        public static List<Vehicle> GetAllVehicles()
        {
            return _Vehicles;
        }

        public static Vehicle? GetVehicle(int vehicleId)
        {
            foreach (var vehicle in _Vehicles)
            {
                if (vehicle.Id == vehicleId)
                {
                    return vehicle;
                }
            }
            return null;
        }

        public static Vehicle CreateVehicle(Vehicle vehicle)
        {
            int id = _Vehicles[_Vehicles.Count - 1].Id;
            vehicle.Id = id + 1;
            _Vehicles.Add(vehicle);
            return vehicle;
        }

        public static Vehicle UpdateVehicle(int vehicleId, Vehicle vehicle)
        {
            var oldVehicle = GetVehicle(vehicleId);
            if (oldVehicle != null)
            {
                oldVehicle.Name = vehicle.Name;
                oldVehicle.Price = vehicle.Price;
            }
            return vehicle;
        }

        public static Vehicle? DeleteVehicle(int vehicleId) 
        {
            var vehicle = GetVehicle(vehicleId);

            if (vehicle != null)
            {
                _Vehicles.Remove(vehicle);
            }
            return vehicle;
        }
    }
}
