using System;
using System.Collections.Generic;
using System.Text;

namespace SerjTm.Sample.TourSearcher.Common.Model
{
    public partial class Tour
    {
        public Guid Id { get; private set; }
        public string Provider { get; private set; }
        public Hotel Hotel { get; private set; }
        public string RoomKind { get; private set; }
        public City StartCity { get; private set; }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime StartHotelDate { get; private set; }
        public int Days { get; private set; }
        public decimal PriceByOnePeople { get; private set; }
        public decimal? FullPrice { get; private set; }
        public string Airline { get; private set; }

        public int MaxRoomPeopleCount { get; private set; }
    }


}
