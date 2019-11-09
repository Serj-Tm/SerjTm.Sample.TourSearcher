using SerjTm.Sample.Common.Model;
using SerjTm.Sample.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerjTm.Sample.TourSearcher.Common.Specifications
{
    public static class SearchOrderSpecification
    {
        public static IOrderedEnumerable<Tour> OrderBy(this IEnumerable<Tour> items, SearchOrder? order)
        {
            switch (order ?? SearchOrder.byName)
            {
                case SearchOrder.byName:
                    return items.OrderBy(item => item.Hotel.Name);
                case SearchOrder.byPrice:
                    return items.OrderBy(item => item.FullPrice);
                case SearchOrder.byPriceDesc:
                    return items.OrderByDescending(item => item.FullPrice);
                case SearchOrder.byDate:
                    return items.OrderBy(item => item.StartDate);
                case SearchOrder.byDateDesc:
                    return items.OrderByDescending(item => item.StartDate);
                default:
                    throw new ArgumentException($"Значение {order} не поддерживается(не реализовано)", "order");
            }
        }
    }
}
