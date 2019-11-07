using System;
using System.Collections.Generic;
using System.Text;

namespace SerjTm.Sample.Common.Model
{
    public partial class Hotel
    {
        public readonly Guid Id;
        public readonly string Name;
        public readonly string Address;
        public readonly City City;
        public readonly int BuildingYear;
    }
}
