using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using NitroBolt.Functional;
using NitroBolt.Immutable;
using System;
using System.Collections.Generic;
using System.Text;

namespace SerjTm.Sample.Common.Model
{
    partial class City
    {
        public City(int id, Country country, string name)
        {
            Id = id;
            Country = country;
            Name = name;
        }

        public City With(int ? id = null, Country country = null, string name = null)
        {
            return new City(id ?? Id, country ?? Country, name ?? Name);
        }
    }

    public static partial class CityHelper
    {
        public static City By(this IEnumerable<City> items, int ? id = null, Country country = null, string name = null)
        {
            if (id != null)
                return items.FirstOrDefault(_item => _item.Id == id);
            if (country != null)
                return items.FirstOrDefault(_item => _item.Country == country);
            if (name != null)
                return items.FirstOrDefault(_item => _item.Name == name);
            return null;
        }
    }

    partial class City_Id
    {
        public City_Id(int id)
        {
            Id = id;
        }

        public City_Id With(int ? id = null)
        {
            return new City_Id(id ?? Id);
        }
    }

    public static partial class City_IdHelper
    {
        public static City_Id By(this IEnumerable<City_Id> items, int ? id = null)
        {
            if (id != null)
                return items.FirstOrDefault(_item => _item.Id == id);
            return null;
        }
    }
}