using System;

namespace MixTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Please change the file path to the vehicle position .dat file");

            var output = new VehicleWorker().NearestVehicle();

            foreach (var location in output)
            {
                Console.WriteLine();
                Console.WriteLine("Position ID: " +location.closestVehicle);
                Console.WriteLine($"Distance: " + location.distance);

            }

        }
    }
}
