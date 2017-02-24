using System;
using System.Collections.Generic;
using System.Windows.Data;
using SmartCA.Application;
using SmartCA.Infrastructure.UI;
using SmartCA.Model.Companies;
using SmartCA.Model.Contacts;
using SmartCA.Model.Projects;

namespace SmartCA.Presentation.ViewModels
{
    public class ProjectContactViewModel : AddressesViewModel<ProjectContact>
    {
        #region Constants

        private static class Constants
        {
            public const string CurrentContactPropertyName = "CurrentContact";
        }

        #endregion

        #region Private Fields

        private CollectionView companies;

        #endregion

        #region Constructors

        public ProjectContactViewModel()
            : this(null)
        {
        }

        public ProjectContactViewModel(IView view)
            : base(view)
        {
            this.companies = new CollectionView(CompanyService.GetAllCompanies());
        }

        #endregion

        #region Properties

        public CollectionView Companies
        {
            get { return this.companies; }
        }

        #endregion

        #region BuildNewEntity

        protected override ProjectContact BuildNewEntity()
        {
            return new ProjectContact(UserSession.CurrentProject, 
                                         null, new Contact(null, 
                                                   "{First Name}", "{Last Name}"));
        }

        #endregion

        #region GetEntitiesList

        protected override List<ProjectContact> GetEntitiesList()
        {
            return new List<ProjectContact>(UserSession.CurrentProject.Contacts);
        }

        #endregion

        #region SaveCurrentEntity

        protected override void SaveCurrentEntity(object sender, EventArgs e)
        {
            this.CurrentEntity.Contact.Addresses.Clear();
            foreach (MutableAddress address in this.Addresses)
            {
                this.CurrentEntity.Contact.Addresses.Add(address.ToAddress());
            }
            ProjectService.SaveProjectContact(this.CurrentEntity);
        }

        #endregion

        #region SetCurrentEntity

        protected override void SetCurrentEntity(ProjectContact entity)
        {
            this.OnPropertyChanged(Constants.CurrentContactPropertyName);
            this.PopulateAddresses(entity.Contact);
        }

        #endregion

        #region Helper Methods

        #endregion
    }
}
