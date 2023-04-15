using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MixTest
{
    public class VehicleWorker
    {
        public static List<VehiclePosition> ReadBinaryData(string filePath)
        {
            List<VehiclePosition> vehiclePositions = new List<VehiclePosition>();

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    while (br.BaseStream.Position != br.BaseStream.Length)
                    {
                        VehiclePosition position = new VehiclePosition();
                        position.PositionId = br.ReadInt32();

                        // Read Vehicle Registration (null terminated string)
                        StringBuilder vehicleRegBuilder = new StringBuilder();
                        char currentChar;
                        while ((currentChar = br.ReadChar()) != '\0')
                        {
                            vehicleRegBuilder.Append(currentChar);
                        }
                        position.VehicleRegistration = vehicleRegBuilder.ToString();

                        position.Latitude = br.ReadSingle();
                        position.Longitude = br.ReadSingle();
                        // Not included in the question
                        position.UTCTimestamp = br.ReadUInt64();

                        vehiclePositions.Add(position);
                    }
                }
            }

            return vehiclePositions;
        }

        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2));
        }

        public Point[] NearestVehicle()
        {
            // Read binary data and store vehicle positions
            var vehiclePositions = ReadBinaryData("VehiclePositions.dat");
            Point[] placesToFind = { 
                new Point(34.544909, -102.100843),
                 new Point(32.345544, -99.123124),
                 new Point(33.234235, -100.214124),
                 new Point(35.195739, -95.348899 ),
                 new Point(31.895839, -97.789573),
                 new Point(32.895839, -101.789573),
                 new Point(34.115839, -100.225732),
                 new Point(32.335839, -99.992232),
                 new Point(33.535339, -94.792232),
                 new Point(32.234235, -100.222222)
                 };


            foreach (var position in vehiclePositions)
            {
                foreach (var location in placesToFind)
                {
                    var dist = Distance(location, new Point(position.Latitude, position.Longitude) );

                    if (location.closestVehicle == null )
                    {
                        location.distance = dist;
                        location.closestVehicle = position.PositionId;
                    }

                    if (location.distance > dist)
                    {
                        location.distance = dist;
                        location.closestVehicle = position.PositionId;
                    }
                }
            }

            return placesToFind;
        }

    }
}
