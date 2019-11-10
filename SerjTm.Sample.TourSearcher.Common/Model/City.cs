using System;
using System.Collections.Generic;
using System.Text;

namespace SerjTm.Sample.TourSearcher.Common.Model
{
    public partial class City: ICity_Id
    {
        public readonly int Id;
        public readonly Country Country;
        public readonly string Name;

        int ICity_Id.Id => Id;
    }

    public interface ICity_Id
    {
        int Id { get; }
    }

    public partial class City_Id: ICity_Id
    {
        public readonly int Id;

        int ICity_Id.Id => Id;
    }
}
