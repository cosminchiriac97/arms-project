using System;

namespace Data.Model
{
    public class House
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public string Neighborhood { get; set; }
        public int LivingSurface { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int Floor { get; set; }
        public int TotalNumberOfFloors { get; set; }
        public string Comfort { get; set; }
        public string Partitioning { get; set; }
        public int YearBuilt { get; set; }
        public string PropertyStatus { get; set; }
        public string PropertyType { get; set; }
        public string FurnishedAndFit { get; set; }
        public bool PossibilityOfParking { get; set; }
        public int NumberOfParkingSpaces { get; set; }
    }
}
