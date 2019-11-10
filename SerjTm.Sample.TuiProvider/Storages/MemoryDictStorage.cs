using SerjTm.Sample.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace SerjTm.Sample.TourSearcher.TuiProvider.Storages
{
    public class MemoryDictStorage
    {
        public MemoryDictStorage(ImmutableArray<Country> countries, ImmutableArray<City> cities, ImmutableArray<Hotel> hotels)
        {
            this.Countries = countries;
            this.Cities = cities;
            this.Hotels = hotels;
        }
        public MemoryDictStorage((ImmutableArray<Country> countries, ImmutableArray<City> cities, ImmutableArray<Hotel> hotels) props)
            :this(props.countries, props.cities, props.hotels)
        {

        }

        public readonly ImmutableArray<Country> Countries;
        public readonly ImmutableArray<City> Cities;
        public readonly ImmutableArray<Hotel> Hotels;


 
    }

}
