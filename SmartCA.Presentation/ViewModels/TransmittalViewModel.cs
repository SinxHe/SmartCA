using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Infrastructure.UI;
using SmartCA.Model;
using SmartCA.Model.Submittals;
using SmartCA.Model.Transmittals;

namespace SmartCA.Presentation.ViewModels
{
    public abstract class TransmittalViewModel<T> : EditableViewModel<T>
        where T : EntityBase, ITransmittal
    {
        #region Constants

        private static class Constants
        {
            public const string MutableCopyToListPropertyName
                = "MutableCopyToList";
        }

        #endregion

        #region Private Fields

        private IList<SpecificationSection> specificationSections;
        private IList<ItemStatus> itemStatuses;
        private BindingList<MutableCopyTo> mutableCopyToList;
        private CollectionView deliveryMethods;
        private IList<Discipline> disciplines;
        private DelegateCommand deleteCopyToCommand;

        #endregion

        #region Constructors

        public TransmittalViewModel()
            : this(null)
        {
        }

        public TransmittalViewModel(IView view) 
            : base(view)
        {
            this.specificationSections 
                = SubmittalService.GetSpecificationSections();
            this.itemStatuses = SubmittalService.GetItemStatuses();
            this.mutableCopyToList = new BindingList<MutableCopyTo>();
            this.deliveryMethods = new CollectionView(
                                       Enum.GetNames(typeof(Delivery)));
            this.disciplines = SubmittalService.GetDisciplines();
            this.deleteCopyToCommand = 
                new DelegateCommand(this.DeleteCopyToCommandHandler);
        }

        #endregion

        #region Properties

        public IList<SpecificationSection> SpecificationSections
        {
            get { return this.specificationSections; }
        }

        public IList<ItemStatus> ItemStatuses
        {
            get { return this.itemStatuses; }
        }

        public BindingList<MutableCopyTo> MutableCopyToList
        {
            get { return this.mutableCopyToList; }
        }

        public CollectionView DeliveryMethods
        {
            get { return this.deliveryMethods; }
        }

        public IList<Discipline> Disciplines
        {
            get { return this.disciplines; }
        }

        public DelegateCommand DeleteCopyToCommand
        {
            get { return this.deleteCopyToCommand; }
        }

        #endregion

        #region Command Handlers

        protected override void NewCommandHandler(object sender, EventArgs e)
        {
            this.mutableCopyToList.Clear();
            base.NewCommandHandler(sender, e);
        }

        private void DeleteCopyToCommandHandler(object sender,
            DelegateCommandEventArgs e)
        {
            MutableCopyTo copyTo = e.Parameter as MutableCopyTo;
            if (copyTo != null)
            {
                this.mutableCopyToList.Remove(copyTo);
            }
        }

        #endregion

        #region Protected Methods

        protected override void SaveCurrentEntity(object sender, EventArgs e)
        {
            this.CurrentEntity.CopyToList.Clear();
            foreach (MutableCopyTo copyTo in this.mutableCopyToList)
            {
                this.CurrentEntity.CopyToList.Add(copyTo.ToCopyTo());
            }
        }

        protected override void SetCurrentEntity(T entity)
        {
            this.OnPropertyChanged("Status");
            this.PopulateTransmittalChildren();
        }

        #endregion

        #region Helper Methods

        protected virtual void PopulateTransmittalChildren()
        {
            this.PopulateMutableCopyToList();
        }

        private void PopulateMutableCopyToList()
        {
            if (this.CurrentEntity != null)
            {
                this.mutableCopyToList.Clear();
                foreach (CopyTo copyTo in this.CurrentEntity.CopyToList)
                {
                    this.mutableCopyToList.Add(new MutableCopyTo(copyTo));
                }
                this.OnPropertyChanged(Constants.MutableCopyToListPropertyName);
            }
        }

        #endregion
    }
}
