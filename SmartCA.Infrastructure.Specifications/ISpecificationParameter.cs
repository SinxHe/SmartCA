using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCA.Infrastructure.Specifications
{
    public interface ISpecificationParameter
    {
        string ParameterName { get; }
        object ParameterValue { get; }
    }
}
