using System;
using System.Collections.Generic;
using System.Text;

namespace SerjTm.Sample.TourSearcher.Common.Model
{
    public class RoomKind
    {
        public readonly static string Standard = "standard";
        public readonly static string Deluxe = "deluxe";
        public readonly static string Luxe = "luxe";


        public readonly static string[] All = new[] { Standard, Deluxe, Luxe };
    }
}
