using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using NitroBolt.Functional;
using NitroBolt.Immutable;
using System;
using System.Collections.Generic;
using System.Text;

namespace SerjTm.Sample.TourSearcher.Common.Model
{
    partial class Tour
    {
        public Tour(Guid id, string provider, Hotel hotel, string roomKind, City startCity, DateTime startDate, DateTime endDate, DateTime startHotelDate, int days, decimal priceByOnePeople, decimal ? fullPrice, string airline, int maxRoomPeopleCount)
        {
            Id = id;
            Provider = provider;
            Hotel = hotel;
            RoomKind = roomKind;
            StartCity = startCity;
            StartDate = startDate;
            EndDate = endDate;
            StartHotelDate = startHotelDate;
            Days = days;
            PriceByOnePeople = priceByOnePeople;
            FullPrice = fullPrice;
            Airline = airline;
            MaxRoomPeopleCount = maxRoomPeopleCount;
        }

        public Tour With(Guid? id = null, string provider = null, Hotel hotel = null, string roomKind = null, City startCity = null, DateTime? startDate = null, DateTime? endDate = null, DateTime? startHotelDate = null, int ? days = null, decimal ? priceByOnePeople = null, decimal ? fullPrice = null, string airline = null, int ? maxRoomPeopleCount = null)
        {
            return new Tour(id ?? Id, provider ?? Provider, hotel ?? Hotel, roomKind ?? RoomKind, startCity ?? StartCity, startDate ?? StartDate, endDate ?? EndDate, startHotelDate ?? StartHotelDate, days ?? Days, priceByOnePeople ?? PriceByOnePeople, fullPrice ?? FullPrice, airline ?? Airline, maxRoomPeopleCount ?? MaxRoomPeopleCount);
        }
    }

    public static partial class TourHelper
    {
        public static Tour By(this IEnumerable<Tour> items, Guid? id = null, string provider = null, Hotel hotel = null, string roomKind = null, City startCity = null, DateTime? startDate = null, DateTime? endDate = null, DateTime? startHotelDate = null, int ? days = null, decimal ? priceByOnePeople = null, decimal ? fullPrice = null, string airline = null, int ? maxRoomPeopleCount = null)
        {
            if (id != null)
                return items.FirstOrDefault(_item => _item.Id == id);
            if (provider != null)
                return items.FirstOrDefault(_item => _item.Provider == provider);
            if (hotel != null)
                return items.FirstOrDefault(_item => _item.Hotel == hotel);
            if (roomKind != null)
                return items.FirstOrDefault(_item => _item.RoomKind == roomKind);
            if (startCity != null)
                return items.FirstOrDefault(_item => _item.StartCity == startCity);
            if (startDate != null)
                return items.FirstOrDefault(_item => _item.StartDate == startDate);
            if (endDate != null)
                return items.FirstOrDefault(_item => _item.EndDate == endDate);
            if (startHotelDate != null)
                return items.FirstOrDefault(_item => _item.StartHotelDate == startHotelDate);
            if (days != null)
                return items.FirstOrDefault(_item => _item.Days == days);
            if (priceByOnePeople != null)
                return items.FirstOrDefault(_item => _item.PriceByOnePeople == priceByOnePeople);
            if (fullPrice != null)
                return items.FirstOrDefault(_item => _item.FullPrice == fullPrice);
            if (airline != null)
                return items.FirstOrDefault(_item => _item.Airline == airline);
            if (maxRoomPeopleCount != null)
                return items.FirstOrDefault(_item => _item.MaxRoomPeopleCount == maxRoomPeopleCount);
            return null;
        }
    }
}