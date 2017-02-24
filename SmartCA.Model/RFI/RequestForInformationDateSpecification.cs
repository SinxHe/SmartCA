using System;
using SmartCA.Infrastructure.Specifications;

namespace SmartCA.Model.RFI
{
    public class RequestForInformationDateSpecification 
        : Specification<RequestForInformation>
    {
        public override bool IsSatisfiedBy(RequestForInformation candidate)
        {
            // Each RFI must have a date received and a date 
            // that the response is needed
            return (candidate.DateReceived.HasValue && 
                candidate.DateRequestedBy.HasValue);
        }
    }
}
