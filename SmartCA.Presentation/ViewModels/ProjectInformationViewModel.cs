using System;
using System.Collections.Generic;
using System.Windows.Data;
using SmartCA.Application;
using SmartCA.Infrastructure.UI;
using SmartCA.Model.Companies;
using SmartCA.Model.Employees;
using SmartCA.Model.Projects;

namespace SmartCA.Presentation.ViewModels
{
    public class ProjectInformationViewModel : EditableViewModel<Project>
    {
        #region Constants

        private static class Constants
        {
            public const string CurrentProjectPropertyName = "CurrentProject";
            public const string ProjectAddressPropertyName = "ProjectAddress";
            public const string OwnerHeadquartersAddressPropertyName = 
                "ProjectOwnerHeadquartersAddress";
        }

        #endregion

        #region Private Fields

        private string newProjectNumber;
        private string newProjectName;
        private MutableAddress projectAddress;
        private MutableAddress projectOwnerHeadquartersAddress;
        private CollectionView owners;
        private CollectionView marketSegments;
        private CollectionView constructionAdministrators;
        private CollectionView principals;
        private DelegateCommand contactsCommand;

        #endregion

        #region Constructors

        public ProjectInformationViewModel()
            : this(null)
        {
        }

        public ProjectInformationViewModel(IView view) 
            : base(view)
        {
            this.CurrentEntity = UserSession.CurrentProject;
            this.newProjectNumber = string.Empty;
            this.newProjectName = string.Empty;
            this.projectAddress = new MutableAddress(this.CurrentEntity.Address);
            this.projectOwnerHeadquartersAddress =
                new MutableAddress(this.CurrentEntity.Owner.HeadquartersAddress);
            this.owners = new CollectionView(CompanyService.GetOwners());
            this.marketSegments = 
                new CollectionView(ProjectService.GetMarketSegments());
            this.constructionAdministrators = 
                new CollectionView(
                    EmployeeService.GetConstructionAdministrators());
            this.principals = new CollectionView(EmployeeService.GetPrincipals());
            this.contactsCommand = new DelegateCommand(this.ContactsCommandHandler);
        }

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return this.CurrentEntity; }
        }

        public string NewProjectNumber
        {
            get { return this.newProjectNumber; }
            set 
            {
                if (this.newProjectNumber != value)
                {
                    this.newProjectNumber = value;
                    this.VerifyNewProject();
                }
            }
        }

        public string NewProjectName
        {
            get { return this.newProjectName; }
            set 
            {
                if (this.newProjectName != value)
                {
                    this.newProjectName = value;
                    this.VerifyNewProject();
                }
            }
        }

        public MutableAddress ProjectAddress
        {
            get { return this.projectAddress; }
        }

        public MutableAddress ProjectOwnerHeadquartersAddress
        {
            get { return this.projectOwnerHeadquartersAddress; }
        }

        public CollectionView Owners
        {
            get { return this.owners; }
        }

        public CollectionView MarketSegments
        {
            get { return this.marketSegments; }
        }

        public CollectionView ConstructionAdministrators
        {
            get { return this.constructionAdministrators; }
        }

        public CollectionView Principals
        {
            get { return this.principals; }
        }

        public DelegateCommand ContactsCommand
        {
            get { return this.contactsCommand; }
        }

        #endregion

        #region Command Handlers

        protected override void NewCommandHandler(object sender, EventArgs e)
        {
            this.projectAddress = new MutableAddress();
            this.OnPropertyChanged(
                Constants.ProjectAddressPropertyName);
            this.newProjectNumber = string.Empty;
            this.newProjectName = string.Empty;
            this.projectOwnerHeadquartersAddress = new MutableAddress();
            this.OnPropertyChanged(
                Constants.OwnerHeadquartersAddressPropertyName);
            this.OnPropertyChanged(
                Constants.CurrentProjectPropertyName);
            base.NewCommandHandler(sender, e);
        }

        private void ContactsCommandHandler(object sender, EventArgs e)
        {
            IView view = ViewFactory.GetView("ProjectContactView");
            view.Show();
        }

        #endregion

        #region Helper Methods

        private void VerifyNewProject()
        {
            if (this.newProjectNumber.Length > 0 && 
                this.newProjectName.Length > 0)
            {
                this.CurrentEntity = new Project(this.newProjectNumber, 
                                          this.newProjectName);
                this.OnPropertyChanged(
                    Constants.CurrentProjectPropertyName);
            }
        }

        #endregion

        #region SaveCurrentEntity

        protected override void SaveCurrentEntity(object sender, EventArgs e)
        {
            this.CurrentEntity.Address = this.projectAddress.ToAddress();
            this.CurrentEntity.Owner.HeadquartersAddress =
                this.projectOwnerHeadquartersAddress.ToAddress();
            ProjectService.SaveProject(this.CurrentEntity);
        }

        #endregion

        #region GetEntitiesList

        protected override List<Project> GetEntitiesList()
        {
            return new List<Project>(ProjectService.GetAllProjects());
        }

        #endregion

        #region SetCurrentEntity

        protected override void SetCurrentEntity(Project entity)
        {
        }

        #endregion

        #region BuildNewEntity

        protected override Project BuildNewEntity()
        {
            return new Project(this.newProjectNumber, this.newProjectName);
        }

        #endregion
    }
}
