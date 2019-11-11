using System;
using System.Collections.Generic;
using System.Text;

namespace SerjTm.Sample.TourSearcher.Common.Model
{
    public partial class City: ICity_Id
    {
        public int Id { get; private set; }
        public Country Country { get; private set; }
        public string Name { get; private set; }
    }

    public interface ICity_Id
    {
        int Id { get; }
    }

    public partial class City_Id: ICity_Id
    {
        public int Id { get; private set; }
    }
}
