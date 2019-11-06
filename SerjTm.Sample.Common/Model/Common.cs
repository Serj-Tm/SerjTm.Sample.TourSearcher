using System;

namespace SerjTm.Sample.Common.Model
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class City
    {
        public Guid Id { get; set; }
        public Country Country { get; set; }
        public string Name { get; set; }
    }

    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public City City { get; set; }
        public int BuildingYear { get; set; }
    }

    public class Tour
    {
        public Guid Id { get; set; }
        public string Provider { get; set; }
        public Hotel Hotel { get; set; }
        public string RoomKind { get; set; }
        public City StartCity { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartHotelDate { get; set; }
        public int Days { get; set; }
        public decimal PriceByPeople { get; set; }
        public decimal? FullPrice { get; set; }
        public string Airline { get; set; }
        public int MaxRoomPeopleCount { get; set; }
    }

}
