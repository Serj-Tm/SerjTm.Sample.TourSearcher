using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using NitroBolt.Functional;
using NitroBolt.Immutable;
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

}