using System;
using System.Collections.Generic;
using SmartCA.Application;
using SmartCA.Infrastructure.UI;
using SmartCA.Model.Employees;
using SmartCA.Model.NumberedProjectChildren;
using SmartCA.Model.Projects;
using SmartCA.Model.ProposalRequests;

namespace SmartCA.Presentation.ViewModels
{
    public class ProposalRequestViewModel :
        TransmittalViewModel<ProposalRequest>
    {
        #region Constants

        #endregion

        #region Private Fields

        private IList<ProjectContact> toList;
        private IList<Employee> fromList;
        
        #endregion

        #region Constructors

        public ProposalRequestViewModel()
            : this(null)
        {
        }

        public ProposalRequestViewModel(IView view) 
            : base(view)
        {
            this.toList = UserSession.CurrentProject.Contacts;
            this.fromList = EmployeeService.GetEmployees();
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

        #endregion

        #region BuildNewEntity

        protected override ProposalRequest BuildNewEntity()
        {
            return NumberedProjectChildFactory.CreateNumberedProjectChild
                <ProposalRequest>(UserSession.CurrentProject);
        }

        #endregion

        #region SaveCurrentEntity

        protected override void SaveCurrentEntity(object sender, EventArgs e)
        {
            base.SaveCurrentEntity(sender, e);
            ProposalRequestService.SaveProposalRequest(
                    this.CurrentEntity);
        }

        #endregion

        #region GetEntitiesList

        protected override List<ProposalRequest> GetEntitiesList()
        {
            return new List<ProposalRequest>(
                       ProposalRequestService.GetProposalRequests(
                       UserSession.CurrentProject));
        }

        #endregion

        #region Helper Methods

        #endregion
    }
}
