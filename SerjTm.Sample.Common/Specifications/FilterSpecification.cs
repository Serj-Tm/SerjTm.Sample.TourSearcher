using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SerjTm.Sample.TourSearcher.Common.Specifications
{
    public class FilterSpecification<T>
    {
        public FilterSpecification(Func<T, bool> f)
        {
            this.f = f;
        }
        readonly Func<T, bool> f;

        public bool IsSatisfiedBy(T item) => f(item);

        public static FilterSpecification<T> operator |(FilterSpecification<T> spec1, FilterSpecification<T> spec2)
            => spec1 == null ? spec2 : spec2 == null ? spec1 : new FilterSpecification<T>(item => spec1.IsSatisfiedBy(item) || spec2.IsSatisfiedBy(item));
        public static FilterSpecification<T> operator &(FilterSpecification<T> spec1, FilterSpecification<T> spec2)
            => spec1 == null ? spec2 : spec2 == null ? spec1 : new FilterSpecification<T>(item => spec1.IsSatisfiedBy(item) && spec2.IsSatisfiedBy(item));
        public static FilterSpecification<T> operator !(FilterSpecification<T> spec)
            => spec == null ? None: new FilterSpecification<T>(item => !spec.IsSatisfiedBy(item));

        public static FilterSpecification<T> All = new FilterSpecification<T>(item => true);
        public static FilterSpecification<T> None = new FilterSpecification<T>(item => false);
    }

    public static class FilterSpecificationHlp
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> items, FilterSpecification<T> specification)
            => specification != null ? items.Where(item => specification.IsSatisfiedBy(item)) : items;
    }

}
