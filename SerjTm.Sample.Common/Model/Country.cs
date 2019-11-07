using System;
using System.Collections.Generic;
using System.Text;

namespace SerjTm.Sample.Common.Model
{
    public partial class Country
    {
        public readonly Guid Id = Guid.NewGuid(); 
        public readonly string Name;
    }
}
