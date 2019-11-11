using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using NitroBolt.Functional;
using NitroBolt.Immutable;
using System.Text;

namespace SerjTm.Sample.TourSearcher.Common.Model
{
    partial class Country
    {
        public Country(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Country With(int ? id = null, string name = null)
        {
            return new Country(id ?? Id, name ?? Name);
        }
    }

}