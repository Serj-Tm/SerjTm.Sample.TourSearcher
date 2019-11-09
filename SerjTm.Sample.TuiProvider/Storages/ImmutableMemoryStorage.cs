using SerjTm.Sample.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SerjTm.Sample.TourSearcher.Imitator;

namespace SerjTm.Sample.TuiProvider.Storages
{
    public class ImmutableMemoryStorage
    {
        public ImmutableMemoryStorage()
        {
            (this.Countries, this.Cities, this.Hotels, this.Tours) = ImitatorService.Imitate("Tui");
        }

        public readonly Country[] Countries;
        public readonly City[] Cities;
        public readonly Hotel[] Hotels;
        public readonly Tour[] Tours;


 
    }

}
