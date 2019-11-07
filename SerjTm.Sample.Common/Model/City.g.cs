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
        public City(Guid id, Country country, string name)
        {
            Id = id;
            Country = country;
            Name = name;
        }

        public City With(Guid? id = null, Country country = null, string name = null)
        {
            return new City(id ?? Id, country ?? Country, name ?? Name);
        }
    }

    public static partial class CityHelper
    {
        public static City By(this IEnumerable<City> items, Guid? id = null, Country country = null, string name = null)
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
}