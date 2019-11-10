using SerjTm.Sample.TourSearcher.Common.Model;
using SerjTm.Sample.TourSearcher.Common.Specifications;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SerjTm.Sample.TourSearcher.Common.Model
{
    public static class TourCategory
    {
        public static FilterSpecification<Tour> City(ICity_Id city) 
            => city != null ? new FilterSpecification<Tour>(tour => tour.Hotel?.City?.Id == city.Id) : All;
        public static FilterSpecification<Tour> StartCity(ICity_Id startCity) 
            => startCity != null ? new FilterSpecification<Tour>(tour => tour.StartCity?.Id == startCity.Id) : All;
        public static FilterSpecification<Tour> StartDate(DateTime? startDate) 
            => startDate != null ? new FilterSpecification<Tour>(tour => startDate <= tour.StartDate) : All;
        public static FilterSpecification<Tour> PeopleCount(int? peopleCount) 
            => peopleCount != null ? new FilterSpecification<Tour>(tour => peopleCount <= tour.MaxRoomPeopleCount) : All;

        public static FilterSpecification<Tour> DaysRange(int? min = null, int? max = null) =>
                min != null && max != null ? new FilterSpecification<Tour>(tour => min <= tour.Days && tour.Days <= max)
                : min != null ? new FilterSpecification<Tour>(tour => min <= tour.Days)
                : max != null ? new FilterSpecification<Tour>(tour => tour.Days <= max)
                : All;


        public static FilterSpecification<Tour> All = FilterSpecification<Tour>.All;


    }
}
