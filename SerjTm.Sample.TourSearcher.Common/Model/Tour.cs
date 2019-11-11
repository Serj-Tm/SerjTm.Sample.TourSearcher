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
        /// <summary>
        /// Город вылета
        /// </summary>
        public City StartCity { get; private set; }
        /// <summary>
        /// Дата вылета
        /// </summary>
        public DateTime StartDate { get; private set; }
        /// <summary>
        /// Дата прилета обратно
        /// </summary>
        public DateTime EndDate { get; private set; }
        /// <summary>
        /// Дата заезда
        /// </summary>
        public DateTime StartHotelDate { get; private set; }
        /// <summary>
        /// Количество ночей
        /// </summary>
        public int Days { get; private set; }
        /// <summary>
        /// Цена за одного человека
        /// </summary>
        public decimal PriceByOnePeople { get; private set; }
        /// <summary>
        /// Полная цена за всех людей в туре
        /// </summary>
        public decimal? FullPrice { get; private set; }
        public string Airline { get; private set; }

        public int MaxRoomPeopleCount { get; private set; }
    }


}
