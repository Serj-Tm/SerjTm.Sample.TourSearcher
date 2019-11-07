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
    partial class Country
    {
        public Country(Guid? id, string name)
        {
            Id = id ?? Id;
            Name = name;
        }

        public Country With(Guid? id = null, string name = null)
        {
            return new Country(id ?? Id, name ?? Name);
        }
    }

    public static partial class CountryHelper
    {
        public static Country By(this IEnumerable<Country> items, Guid? id = null, string name = null)
        {
            if (id != null)
                return items.FirstOrDefault(_item => _item.Id == id);
            if (name != null)
                return items.FirstOrDefault(_item => _item.Name == name);
            return null;
        }
    }
}