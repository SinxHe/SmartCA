using System;
using System.Collections.Generic;
using SmartCA.Infrastructure.UI;
using SmartCA.Model.Companies;

namespace SmartCA.Presentation.ViewModels
{
    public class CompanyViewModel : AddressesViewModel<Company>
    {
        #region Constants

        private static class Constants
        {
            public const string HeadquartersAddressPropertyName =
                "HeadquartersAddress";
        }

        #endregion

        #region Private Fields

        private MutableAddress headquartersAddress;

        #endregion

        #region Constructors

        public CompanyViewModel()
            : this(null)
        {
        }

        public CompanyViewModel(IView view)
            : base(view)
        {
            this.headquartersAddress = null;
        }

        #endregion

        #region Public Properties

        public MutableAddress HeadquartersAddress
        {
            get { return this.headquartersAddress; }
            set 
            {
                if (this.headquartersAddress != value)
                {
                    this.headquartersAddress = value;
                    this.OnPropertyChanged(
                        Constants.HeadquartersAddressPropertyName);
                }
            }
        }

        #endregion

        #region BuildNewEntity

        protected override Company BuildNewEntity()
        {
            Company company = new Company();
            company.Name = "{Enter Company Name}";
            return company;
        }

        #endregion

        #region SaveCurrentEntity

        protected override void SaveCurrentEntity(object sender, EventArgs e)
        {
            this.CurrentEntity.Addresses.Clear();
            foreach (MutableAddress address in this.Addresses)
            {
                this.CurrentEntity.Addresses.Add(address.ToAddress());
            }
            this.CurrentEntity.HeadquartersAddress =
                this.headquartersAddress.ToAddress();
            CompanyService.SaveCompany(this.CurrentEntity);
        }

        #endregion

        #region GetEntitiesList

        protected override List<Company> GetEntitiesList()
        {
            return new List<Company>(CompanyService.GetAllCompanies());
        }

        #endregion

        #region SetCurrentEntity

        protected override void SetCurrentEntity(Company entity)
        {
            this.PopulateAddresses(entity);
            this.HeadquartersAddress =
                new MutableAddress(
                    entity.HeadquartersAddress);
        }

        #endregion

        #region Helper Methods

        #endregion
    }
}
