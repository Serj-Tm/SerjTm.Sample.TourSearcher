using SerjTm.Sample.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace SerjTm.Sample.TourSearcher.TuiProvider.Storages
{
    public class MemoryTourStorage
    {
        public MemoryTourStorage(ImmutableArray<Tour> tours)
        {
            this.Tours = tours;
        }

        public readonly ImmutableArray<Tour> Tours;
    }

}
