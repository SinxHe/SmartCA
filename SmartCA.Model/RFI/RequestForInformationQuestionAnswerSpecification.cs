using System;
using SmartCA.Infrastructure.Specifications;

namespace SmartCA.Model.RFI
{
    public class RequestForInformationQuestionAnswerSpecification 
        : Specification<RequestForInformation>
    {
        public override bool IsSatisfiedBy(RequestForInformation candidate)
        {
            // The RFI must have a question and answer

            // The answer could be the short answer or 
            // the long answer
            return (!string.IsNullOrEmpty(candidate.Question) &&
                (!string.IsNullOrEmpty(candidate.ShortAnswer) ||
                !string.IsNullOrEmpty(candidate.LongAnswer)));
        }
    }
}
