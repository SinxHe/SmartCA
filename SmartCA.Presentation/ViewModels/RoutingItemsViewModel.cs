using System.ComponentModel;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Infrastructure.UI;
using SmartCA.Model.Transmittals;

namespace SmartCA.Presentation.ViewModels
{
    public class RoutingItemsViewModel<T> where T : EntityBase, IHasRoutingItems
    {
        #region Constants

        private static class Constants
        {
            public const string RoutingItemsPropertyName
                    = "RoutingItems";
        }

        #endregion

        #region Private Fields

        private BindingList<RoutingItem> routingItems;
        private DelegateCommand deleteRoutingItemCommand;
        private EditableViewModel<T> viewModel;

        #endregion

        #region Constructors

        public RoutingItemsViewModel(EditableViewModel<T> viewModel)
        {
            this.routingItems = new BindingList<RoutingItem>();
            this.deleteRoutingItemCommand =
                new DelegateCommand(this.DeleteRoutingItemCommandHandler);
            this.viewModel = viewModel;
        }

        #endregion

        #region Properties

        public BindingList<RoutingItem> RoutingItems
        {
            get { return this.routingItems; }
        }

        public DelegateCommand DeleteRoutingItemCommand
        {
            get { return this.deleteRoutingItemCommand; }
        }

        #endregion

        #region Command Handlers

        private void DeleteRoutingItemCommandHandler(object sender,
            DelegateCommandEventArgs e)
        {
            RoutingItem item = e.Parameter as RoutingItem;
            if (item != null)
            {
                this.routingItems.Remove(item);
            }
        }

        #endregion

        #region Methods

        public void ClearBoundRoutingItems()
        {
            this.routingItems.Clear();
        }

        public void AddBoundRoutingItemsToEntity()
        {
            if (this.viewModel.CurrentEntity != null)
            {
                this.viewModel.CurrentEntity.RoutingItems.Clear();
                foreach (RoutingItem item in this.routingItems)
                {
                    this.viewModel.CurrentEntity.RoutingItems.Add(item);
                }
            }
        }

        public void AddEntityRoutingItemsToBoundRoutingItems()
        {
            if (this.viewModel.CurrentEntity != null)
            {
                this.routingItems.Clear();
                foreach (RoutingItem item in this.viewModel.CurrentEntity.RoutingItems)
                {
                    this.routingItems.Add(item);
                }
                this.viewModel.OnPropertyChanged(Constants.RoutingItemsPropertyName);
            }
        }

        #endregion
    }
}
