using System;
using System.Collections.Generic;
using SmartCA.Application;
using SmartCA.Infrastructure.UI;
using SmartCA.Model.NumberedProjectChildren;
using SmartCA.Model.Projects;
using SmartCA.Model.RFI;

namespace SmartCA.Presentation.ViewModels
{
    public class RequestForInformationViewModel 
        : RoutableTransmittalViewModel<RequestForInformation>
    {
        #region Private Fields

        private IList<ProjectContact> toList;
        private IList<ProjectContact> fromList;

        #endregion

        #region Constructors

        public RequestForInformationViewModel()
            : this(null)
        {
        }

        public RequestForInformationViewModel(IView view) 
            : base(view)
        {
            this.toList = UserSession.CurrentProject.Contacts;
            this.fromList = UserSession.CurrentProject.Contacts;
        }

        #endregion

        #region Properties

        public IList<ProjectContact> ToList
        {
            get { return this.toList; }
        }

        public IList<ProjectContact> FromList
        {
            get { return this.fromList; }
        }

        #endregion

        #region BuildNewEntity

        protected override RequestForInformation BuildNewEntity()
        {
            return NumberedProjectChildFactory.CreateNumberedProjectChild
                <RequestForInformation>(UserSession.CurrentProject);
        }

        #endregion

        #region GetEntitiesList

        protected override List<RequestForInformation> GetEntitiesList()
        {
            return new List<RequestForInformation>(RequestForInformationService.GetRequestsForInformation(
                                      UserSession.CurrentProject));
        }

        #endregion

        #region SaveCurrentEntity

        protected override void SaveCurrentEntity(object sender, EventArgs e)
        {
            base.SaveCurrentEntity(sender, e);
            RequestForInformationService.SaveRequestForInformation(this.CurrentEntity);
        }

        #endregion

        #region Helper Methods

        #endregion
    }
}
