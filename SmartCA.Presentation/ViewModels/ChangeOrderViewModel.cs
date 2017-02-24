using System;
using System.Collections.Generic;
using System.Windows.Data;
using SmartCA.Application;
using SmartCA.Infrastructure.UI;
using SmartCA.Model;
using SmartCA.Model.ChangeOrders;
using SmartCA.Model.Companies;
using SmartCA.Model.NumberedProjectChildren;
using SmartCA.Model.Projects;
using SmartCA.Model.Submittals;

namespace SmartCA.Presentation.ViewModels
{
    public class ChangeOrderViewModel : EditableViewModel<ChangeOrder>
    {
        #region Constants

        #endregion

        #region Private Fields

        private IList<Company> contractors;
        private IList<ProjectContact> toList;
        private CollectionView priceChangeTypesView;
        private CollectionView priceChangeDirections;
        private CollectionView timeChangeDirections;
        private IList<ItemStatus> statuses;
        private IList<Discipline> disciplines;
        private RoutingItemsViewModel<ChangeOrder> routingItemsViewModel;
        
        #endregion

        #region Constructors

        public ChangeOrderViewModel()
            : this(null)
        {
        }

        public ChangeOrderViewModel(IView view) 
            : base(view)
        {
            this.contractors = CompanyService.GetAllCompanies();
            this.toList = UserSession.CurrentProject.Contacts;
            this.priceChangeTypesView = new 
                CollectionView(Enum.GetNames(typeof(PriceChangeType)));
            string[] changeDirections = Enum.GetNames(typeof(ChangeDirection));
            this.priceChangeDirections = new CollectionView(changeDirections);
            this.timeChangeDirections = new CollectionView(changeDirections);
            this.statuses = SubmittalService.GetItemStatuses();
            this.disciplines = SubmittalService.GetDisciplines();
            this.routingItemsViewModel = new RoutingItemsViewModel<ChangeOrder>(this);
        }

        #endregion

        #region Properties

        public IList<Company> Contractors
        {
            get { return this.contractors; }
        }

        public IList<ProjectContact> ToList
        {
            get { return this.toList; }
        }

        public CollectionView PriceChangeTypesView
        {
            get { return this.priceChangeTypesView; }
        }

        public CollectionView PriceChangeDirections
        {
            get { return this.priceChangeDirections; }
        }

        public CollectionView TimeChangeDirections
        {
            get { return this.timeChangeDirections; }
        }

        public IList<ItemStatus> Statuses
        {
            get { return this.statuses; }
        }

        public IList<Discipline> Disciplines
        {
            get { return this.disciplines; }
        }

        public RoutingItemsViewModel<ChangeOrder> RoutingItemsViewModel
        {
            get { return this.routingItemsViewModel; }
        }

        #endregion

        #region Command Handlers

        #endregion

        #region Helper Methods

        #endregion

        #region BuildNewEntity

        protected override ChangeOrder BuildNewEntity()
        {
            this.routingItemsViewModel.ClearBoundRoutingItems();
            return NumberedProjectChildFactory.CreateNumberedProjectChild<ChangeOrder>(UserSession.CurrentProject);
        }

        #endregion

        #region GetEntitiesList

        protected override List<ChangeOrder> GetEntitiesList()
        {
            return new List<ChangeOrder>(
                ChangeOrderService.GetChangeOrders(UserSession.CurrentProject));
        }

        #endregion

        #region SaveCurrentEntity

        protected override void SaveCurrentEntity(object sender, EventArgs e)
        {
            this.routingItemsViewModel.AddBoundRoutingItemsToEntity();
            ChangeOrderService.SaveChangeOrder(this.CurrentEntity);
        }

        #endregion

        #region SetCurrentEntity

        protected override void SetCurrentEntity(ChangeOrder entity)
        {
            this.routingItemsViewModel.AddEntityRoutingItemsToBoundRoutingItems();
        }

        #endregion
    }
}
