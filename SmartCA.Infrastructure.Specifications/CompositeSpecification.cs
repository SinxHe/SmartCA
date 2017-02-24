using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace SmartCA.Infrastructure.Specifications
{
    public abstract class CompositeSpecification<TCandidate> : Specification<TCandidate>
    {
        private List<ISpecification<TCandidate>> components;
        
        public CompositeSpecification()
        {
            this.components = new List<ISpecification<TCandidate>>();
        }

        #region Specification<TCandidate> Members

        public abstract override bool IsSatisfiedBy(TCandidate candidate);

        #endregion

        public ReadOnlyCollection<ISpecification<TCandidate>> Components
        {
            get { return new ReadOnlyCollection<ISpecification<TCandidate>>(this.components); }
        }

        public AndSpecification<TCandidate> And(ISpecification<TCandidate> other)
        {
            return new AndSpecification<TCandidate>(this, other);
        }

        public OrSpecification<TCandidate> Or(ISpecification<TCandidate> other)
        {
            return new OrSpecification<TCandidate>(this, other);
        }

        public InverseSpecification<TCandidate> Not()
        {
            return new InverseSpecification<TCandidate>(this);
        }

        public IDictionary<string, object> GetSpecificationParameters()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            ISpecificationParameter parameter;
            foreach (ISpecification<TCandidate> component in this.components)
            {
                if (component is ISpecificationParameter)
                {
                    parameter = component as ISpecificationParameter;
                    parameters.Add(parameter.ParameterName, parameter.ParameterValue);
                }
            }
            return parameters;
        }

        protected void AddChildComponents(ISpecification<TCandidate> parentSpecification)
        {
            if (parentSpecification is CompositeSpecification<TCandidate>)
            {
                CompositeSpecification<TCandidate> composite = parentSpecification as CompositeSpecification<TCandidate>;
                if (composite.components.Count > 0)
                {
                    foreach (ISpecification<TCandidate> component in composite.Components)
                    {
                        if (!component.HasBeenAddedToComposite)
                        {
                            if (component is CompositeSpecification<TCandidate>)
                            {
                                this.AddChildComponents(component);
                                component.HasBeenAddedToComposite = true;
                            }
                            else
                            {
                                this.AddChildComponent(component);
                            }
                        }
                        else
                        {
                            this.AddChildComponent(component);
                        }
                    }
                }
                else
                {
                    this.AddChildComponent(parentSpecification);
                }
            }
            else
            {
                this.AddChildComponent(parentSpecification);
            }
        }

        private void AddChildComponent(ISpecification<TCandidate> childSpecification)
        {
            if (!this.components.Contains(childSpecification))
            {
                this.components.Add(childSpecification);
            }
        }
    }
}