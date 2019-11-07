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
    partial class Hotel
    {
        public Hotel(Guid id, string name, string address, City city, int buildingYear)
        {
            Id = id;
            Name = name;
            Address = address;
            City = city;
            BuildingYear = buildingYear;
        }

        public Hotel With(Guid? id = null, string name = null, string address = null, City city = null, int ? buildingYear = null)
        {
            return new Hotel(id ?? Id, name ?? Name, address ?? Address, city ?? City, buildingYear ?? BuildingYear);
        }
    }

    public static partial class HotelHelper
    {
        public static Hotel By(this IEnumerable<Hotel> items, Guid? id = null, string name = null, string address = null, City city = null, int ? buildingYear = null)
        {
            if (id != null)
                return items.FirstOrDefault(_item => _item.Id == id);
            if (name != null)
                return items.FirstOrDefault(_item => _item.Name == name);
            if (address != null)
                return items.FirstOrDefault(_item => _item.Address == address);
            if (city != null)
                return items.FirstOrDefault(_item => _item.City == city);
            if (buildingYear != null)
                return items.FirstOrDefault(_item => _item.BuildingYear == buildingYear);
            return null;
        }
    }
}