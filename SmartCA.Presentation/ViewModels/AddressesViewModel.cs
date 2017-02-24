using System.ComponentModel;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Infrastructure.UI;
using SmartCA.Model.Addresses;

namespace SmartCA.Presentation.ViewModels
{
    public abstract class AddressesViewModel<T> 
        : EditableViewModel<T> where T : EntityBase
    {
        #region Constants

        private static class Constants
        {
            public const string AddressesPropertyName = "Addresses";
        }

        #endregion

        #region Private Fields

        private BindingList<MutableAddress> addresses;
        private DelegateCommand deleteAddressCommand;

        #endregion

        #region Constructors

        protected AddressesViewModel()
            : this(null)
        {
        }

        protected AddressesViewModel(IView view)
            : base(view)
        {
            this.addresses = new BindingList<MutableAddress>();
            this.deleteAddressCommand =
                new DelegateCommand(this.DeleteAddressCommandHandler);
        }

        #endregion

        #region Public Properties

        public BindingList<MutableAddress> Addresses
        {
            get { return this.addresses; }
        }

        public DelegateCommand DeleteAddressCommand
        {
            get { return this.deleteAddressCommand; }
        }

        #endregion

        #region Command Handlers

        private void DeleteAddressCommandHandler(object sender,
            DelegateCommandEventArgs e)
        {
            MutableAddress address = e.Parameter as MutableAddress;
            if (address != null)
            {
                this.addresses.Remove(address);
            }
        }

        #endregion

        #region Protected Methods

        protected void PopulateAddresses(IHasAddresses entity)
        {
            if (this.CurrentEntity != null)
            {
                this.addresses.Clear();
                foreach (Address address in entity.Addresses)
                {
                    this.addresses.Add(new MutableAddress(address));
                }
                this.OnPropertyChanged(Constants.AddressesPropertyName);
            }
        }

        #endregion
    }
}
