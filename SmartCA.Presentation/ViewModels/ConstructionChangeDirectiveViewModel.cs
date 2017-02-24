using System;
using System.Collections.Generic;
using SmartCA.Application;
using SmartCA.Infrastructure.UI;
using SmartCA.Model.Companies;
using SmartCA.Model.ConstructionChangeDirectives;
using SmartCA.Model.Employees;
using SmartCA.Model.NumberedProjectChildren;
using SmartCA.Model.Projects;

namespace SmartCA.Presentation.ViewModels
{
    public class ConstructionChangeDirectiveViewModel :
        TransmittalViewModel<ConstructionChangeDirective>
    {
        #region Constants

        #endregion

        #region Private Fields

        private IList<ProjectContact> toList;
        private IList<Employee> fromList;
        private IList<Company> contractors;
        
        #endregion

        #region Constructors

        public ConstructionChangeDirectiveViewModel()
            : this(null)
        {
        }

        public ConstructionChangeDirectiveViewModel(IView view) 
            : base(view)
        {
            this.toList = UserSession.CurrentProject.Contacts;
            this.fromList = EmployeeService.GetEmployees();
            this.contractors = CompanyService.GetAllCompanies();
        }

        #endregion

        #region Properties

        public IList<ProjectContact> ToList
        {
            get { return this.toList; }
        }

        public IList<Employee> FromList
        {
            get { return this.fromList; }
        }

        public IList<Company> Contractors
        {
            get { return this.contractors; }
        }

        #endregion

        #region BuildNewEntity

        protected override ConstructionChangeDirective BuildNewEntity()
        {
            return NumberedProjectChildFactory.CreateNumberedProjectChild
                <ConstructionChangeDirective>(UserSession.CurrentProject);
        }

        #endregion

        #region SaveCurrentEntity

        protected override void SaveCurrentEntity(object sender, EventArgs e)
        {

            base.SaveCurrentEntity(sender, e);
            ConstructionChangeDirectiveService.SaveConstructionChangeDirective(
                this.CurrentEntity);
        }

        #endregion

        #region GetEntitiesList

        protected override List<ConstructionChangeDirective> GetEntitiesList()
        {
            return new List<ConstructionChangeDirective>(
                       ConstructionChangeDirectiveService.GetConstructionChangeDirectives(
                       UserSession.CurrentProject));
        }

        #endregion

        #region Helper Methods

        #endregion
    }
}
