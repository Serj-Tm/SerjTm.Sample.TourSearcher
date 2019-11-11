using SerjTm.Sample.TourSearcher.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace SerjTm.Sample.TourSearcher.TuiProvider.Storages
{
    public class MemoryDictStorage
    {
        public MemoryDictStorage(ImmutableArray<Country> countries, ImmutableArray<City> cities, ImmutableArray<City> flyCities, ImmutableArray<Hotel> hotels)
        {
            this.Countries = countries;
            this.Cities = cities;
            this.FlyCities = flyCities;
            this.Hotels = hotels;
            this.HotelIndex = hotels.ToImmutableDictionary(hotel => hotel.Id);
        }
        public MemoryDictStorage((ImmutableArray<Country> countries, ImmutableArray<City> cities, ImmutableArray<City> flyCities, ImmutableArray<Hotel> hotels) props)
            :this(props.countries, props.cities, props.flyCities, props.hotels)
        {

        }

        public readonly ImmutableArray<Country> Countries;
        public readonly ImmutableArray<City> Cities;
        /// <summary>
        /// Города вылета и прилета
        /// </summary>
        public readonly ImmutableArray<City> FlyCities;
        public readonly ImmutableArray<Hotel> Hotels;
        public readonly ImmutableDictionary<Guid, Hotel> HotelIndex;


 
    }

}
