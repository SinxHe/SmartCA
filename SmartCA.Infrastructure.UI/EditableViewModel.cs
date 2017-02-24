using System;
using SmartCA.Infrastructure.DomainBase;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Data;
using System.ComponentModel;

namespace SmartCA.Infrastructure.UI
{
    public abstract class EditableViewModel<T>
        : ViewModel, INotifyPropertyChanged where T : EntityBase
    {
        
        #region ObjectState Enum

        public enum ObjectState
        {
            New,
            Existing,
            Deleted
        }

        #endregion

        #region Constants

        private const string currentObjectStatePropertyName = "CurrentObjectState";
        private const string currentEntityPropertyName = "CurrentEntity";

        #endregion

        #region Private Fields

        private ObjectState currentObjectState;
        private IList<BrokenRule> brokenRules;
        private T currentEntity;
        private List<T> entitiesList;
        private CollectionView entitiesView;
        private DelegateCommand saveCommand;
        private DelegateCommand newCommand;

        #endregion

        #region Constructors

        protected EditableViewModel()
            : this(null)
        {
        }

        protected EditableViewModel(IView view) 
            : base(view)
        {
            this.currentObjectState = ObjectState.Existing;
            this.brokenRules = new List<BrokenRule>();
            this.currentEntity = default(T);
            this.entitiesList = this.GetEntitiesList();
            this.entitiesView = new CollectionView(this.entitiesList);
            this.saveCommand = new DelegateCommand(this.SaveCommandHandler);
            this.newCommand = new DelegateCommand(this.NewCommandHandler);
        }

        #endregion

        #region Abstract Methods

        protected abstract T BuildNewEntity();
        protected abstract List<T> GetEntitiesList();
        protected abstract void SaveCurrentEntity(object sender, EventArgs e);
        protected abstract void SetCurrentEntity(T entity);

        #endregion

        #region Public Properties

        public IList<BrokenRule> BrokenRules
        {
            get { return this.brokenRules; }
        }

        public ObjectState CurrentObjectState
        {
            get { return this.currentObjectState; }
            set
            {
                if (this.currentObjectState != value)
                {
                    this.currentObjectState = value;
                    this.OnPropertyChanged(
                        EditableViewModel<T>.currentObjectStatePropertyName);
                }
            }
        }

        public T CurrentEntity
        {
            get { return this.currentEntity; }
            set
            {
                if (this.currentEntity != value)
                {
                    this.currentEntity = value;
                    this.SetCurrentEntity(value);
                    this.saveCommand.IsEnabled = (this.currentEntity != null);
                    this.OnPropertyChanged(
                        EditableViewModel<T>.currentEntityPropertyName);
                }
            }
        }

        protected List<T> EntitiesList
        {
            get { return this.entitiesList; }
        }

        public CollectionView EntitiesView
        {
            get { return this.entitiesView; }
        }

        public DelegateCommand SaveCommand
        {
            get { return this.saveCommand; }
        }

        public DelegateCommand NewCommand
        {
            get { return this.newCommand; }
        }

        #endregion

        #region ValidateCurrentObject

        protected bool ValidateCurrentObject()
        {
            this.brokenRules.Clear();
            ReadOnlyCollection<BrokenRule> currentObjectBrokenRules = 
                this.currentEntity.GetBrokenRules();
            foreach (BrokenRule rule in currentObjectBrokenRules)
            {
                this.brokenRules.Add(rule);
            }
            return (this.brokenRules.Count == 0);
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region OnPropertyChanged

        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Command Handlers

        protected void SaveCommandHandler(object sender, EventArgs e)
        {
            if (this.ValidateCurrentObject())
            {
                this.SaveCurrentEntity(sender, e);
                this.CurrentObjectState = ObjectState.Existing;
            }
        }

        protected virtual void NewCommandHandler(object sender, EventArgs e)
        {
            this.CurrentObjectState = ObjectState.New;
            this.brokenRules.Clear();
            this.entitiesList.Add(this.BuildNewEntity());
            this.currentEntity = null;
            this.entitiesView.Refresh();
            this.entitiesView.MoveCurrentToLast();
        }

        #endregion
    }
}
