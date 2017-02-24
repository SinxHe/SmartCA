using System;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Infrastructure.UI;
using SmartCA.Model.Transmittals;

namespace SmartCA.Presentation.ViewModels
{
    public abstract class RoutableTransmittalViewModel<T> : TransmittalViewModel<T>
        where T : EntityBase, IRoutableTransmittal
    {
        #region Constants

        #endregion

        #region Private Fields

        private RoutingItemsViewModel<T> routingItemsViewModel;

        #endregion

        #region Constructors

        public RoutableTransmittalViewModel()
            : this(null)
        {
        }

        public RoutableTransmittalViewModel(IView view) 
            : base(view)
        {
            this.routingItemsViewModel = new RoutingItemsViewModel<T>(this);
        }

        #endregion

        #region Properties

        public RoutingItemsViewModel<T> RoutingItemsViewModel
        {
            get { return this.routingItemsViewModel; }
        }

        #endregion

        #region Command Handlers

        protected override void NewCommandHandler(object sender, EventArgs e)
        {
            this.routingItemsViewModel.ClearBoundRoutingItems();
            base.NewCommandHandler(sender, e);
        }

        #endregion

        #region PopulateTransmittalChildren

        protected override void PopulateTransmittalChildren()
        {
            base.PopulateTransmittalChildren();
            this.routingItemsViewModel.AddEntityRoutingItemsToBoundRoutingItems();
        }

        #endregion

        #region SaveCurrentEntity

        protected override void SaveCurrentEntity(object sender, EventArgs e)
        {
            base.SaveCurrentEntity(sender, e);
            this.routingItemsViewModel.AddBoundRoutingItemsToEntity();
        }

        #endregion
    }
}
