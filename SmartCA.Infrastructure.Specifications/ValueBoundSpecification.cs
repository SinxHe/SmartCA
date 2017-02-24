using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCA.Infrastructure.Specifications
{
    public abstract class ValueBoundSpecification<TCandidate, TValue> : LeafSpecification<TCandidate>, ISpecificationParameter
    {
        private string attributeName;
        private string[] parsedAttributeName;
        private TValue attributeValue;

        public ValueBoundSpecification(string attributeName, TValue attributeValue)
        {
            this.attributeName = attributeName;
            this.attributeValue = attributeValue;
            this.ParseAttributeName();
        }

        private void ParseAttributeName()
        {
            this.parsedAttributeName = this.attributeName.Split('.');
        }

        public string AttributeName
        {
            get { return this.attributeName; }
        }

        public TValue AttributeValue
        {
            get { return this.attributeValue; }
        }

        protected string GetCandidateStringValue(TCandidate candidate)
        {
            return this.GetCandidateObjectValue(candidate).ToString();
        }

        protected TValue GetCandidateTValue(TCandidate candidate)
        {
            return (TValue) this.GetCandidateObjectValue(candidate);
        }

        private object GetCandidateObjectValue(TCandidate candidate)
        {
            object candidateObjectValue = candidate;
            foreach (string attributeName in this.parsedAttributeName)
            {
                candidateObjectValue = candidateObjectValue.GetType().GetProperty(attributeName).GetValue(candidateObjectValue, null);
            }
            return candidateObjectValue;
        }

        public abstract override bool IsSatisfiedBy(TCandidate candidate);


        #region IValueBound Members

        public string ParameterName
        {
            get { return this.attributeName; }
        }

        public object ParameterValue
        {
            get { return this.attributeValue as object; }
        }

        #endregion
    }
}
