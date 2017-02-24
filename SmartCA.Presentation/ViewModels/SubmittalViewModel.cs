using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using SmartCA.Application;
using SmartCA.Infrastructure.UI;
using SmartCA.Model.Employees;
using SmartCA.Model.Projects;
using SmartCA.Model.Submittals;

namespace SmartCA.Presentation.ViewModels
{
    public class SubmittalViewModel :
        RoutableTransmittalViewModel<Submittal>
    {
        #region Constants

        private static class Constants
        {
            public const string TrackingItemsPropertyName
                = "TrackingItems";
        }

        #endregion

        #region Private Fields

        private IList<ProjectContact> toList;
        private BindingList<TrackingItem> trackingItems;
        private IList<Employee> fromList;
        private CollectionView trackingStatusValues;
        private DelegateCommand deleteTrackingItemCommand;

        #endregion

        #region Constructors

        public SubmittalViewModel()
            : this(null)
        {
        }

        public SubmittalViewModel(IView view) 
            : base(view)
        {
            this.toList = UserSession.CurrentProject.Contacts;
            this.trackingItems = new BindingList<TrackingItem>();
            this.fromList = EmployeeService.GetEmployees();
            this.trackingStatusValues = new CollectionView(
                                            Enum.GetNames(typeof(ActionStatus)));
            this.deleteTrackingItemCommand = 
                new DelegateCommand(this.DeleteTrackingItemCommandHandler);
        }

        #endregion

        #region Properties

        public IList<ProjectContact> ToList
        {
            get { return this.toList; }
        }

        public BindingList<TrackingItem> TrackingItems
        {
            get { return this.trackingItems; }
        }

        public IList<Employee> FromList
        {
            get { return this.fromList; }
        }

        public CollectionView TrackingStatusValues
        {
            get { return this.trackingStatusValues; }
        }

        public DelegateCommand DeleteTrackingItemCommand
        {
            get { return this.deleteTrackingItemCommand; }
        }

        #endregion

        #region Command Handlers

        private void DeleteTrackingItemCommandHandler(object sender,
            DelegateCommandEventArgs e)
        {
            TrackingItem item = e.Parameter as TrackingItem;
            if (item != null)
            {
                this.trackingItems.Remove(item);
            }
        }

        #endregion

        #region SaveCurrentEntity

        protected override void SaveCurrentEntity(object sender, EventArgs e)
        {
            base.SaveCurrentEntity(sender, e);
            this.CurrentEntity.TrackingItems.Clear();
            foreach (TrackingItem item in this.trackingItems)
            {
                this.CurrentEntity.TrackingItems.Add(item);
            }
            SubmittalService.SaveSubmittal(this.CurrentEntity);
        }

        #endregion

        #region BuildNewEntity

        protected override Submittal BuildNewEntity()
        {
            this.trackingItems.Clear();
            Submittal newSubmittal = new Submittal(
                                         this.CurrentEntity.SpecSection,
                                         this.CurrentEntity.ProjectKey);
            newSubmittal.SpecSectionSecondaryIndex = "01";
            return newSubmittal;
        }

        #endregion

        #region PopulateTransmittalChildren

        protected override void PopulateTransmittalChildren()
        {
            base.PopulateTransmittalChildren();
            this.PopulateTrackingItems();
        }

        #endregion

        #region GetEntitiesList

        protected override List<Submittal> GetEntitiesList()
        {
            return new List<Submittal>(SubmittalService.GetSubmittals(
                                      UserSession.CurrentProject));
        }

        #endregion

        #region Helper Methods

        private void PopulateTrackingItems()
        {
            if (this.CurrentEntity != null)
            {
                this.trackingItems.Clear();
                foreach (TrackingItem item in this.CurrentEntity.TrackingItems)
                {
                    this.trackingItems.Add(item);
                }
                this.OnPropertyChanged(Constants.TrackingItemsPropertyName);
            }
        }

         #endregion
    }
}
