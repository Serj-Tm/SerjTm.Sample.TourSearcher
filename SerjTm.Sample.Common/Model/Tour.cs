using System;
using System.Collections.Generic;
using System.Text;

namespace SerjTm.Sample.TourSearcher.Common.Model
{
    public partial class Tour
    {
        public readonly Guid Id;
        public readonly string Provider;
        public readonly Hotel Hotel;
        public readonly string RoomKind;
        public readonly City StartCity;

        public readonly DateTime StartDate;
        public readonly DateTime EndDate;
        public readonly DateTime StartHotelDate;
        public readonly int Days;
        public readonly decimal PriceByOnePeople;
        public readonly decimal? FullPrice;
        public readonly string Airline;

        public readonly int MaxRoomPeopleCount;
    }


}
