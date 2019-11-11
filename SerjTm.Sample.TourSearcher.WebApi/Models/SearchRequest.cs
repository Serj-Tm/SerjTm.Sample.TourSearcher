using SerjTm.Sample.TourSearcher.Common.Model;
using SerjTm.Sample.TourSearcher.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerjTm.Sample.TourSearcher.WebApi.Models
{
    public class SearchRequest
    {
        public City_Id StartCity;
        public City_Id City;
        public DateTime? StartDate;
        public int? MinDays;
        public int? MaxDays;
        public int? PeopleCount;
        public SearchOrder? Order;
    }
    public class SearchRequest_Hotel
    {
        public Guid Id;
    }
}
