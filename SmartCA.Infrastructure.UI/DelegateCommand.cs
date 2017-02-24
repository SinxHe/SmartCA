using System;
using System.Windows.Input;

namespace SmartCA.Infrastructure.UI
{
    public class DelegateCommand : ICommand
    {
        public delegate void SimpleEventHandler(object sender, DelegateCommandEventArgs e);

        private SimpleEventHandler handler;
        private bool isEnabled = true;

        public DelegateCommand(SimpleEventHandler handler)
        {
            this.handler = handler;
        }

        #region ICommand implementation

        /// <summary>
        /// Executing the command is as simple as calling that method 
        /// we were handed on creation.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the 
        /// command does not require data to be passed,
        /// this object can be set to null.</param>
        public void Execute(object parameter)
        {
            this.handler(this, new DelegateCommandEventArgs(parameter));
        }

        /// <summary>
        /// Determines whether the command can execute in its
        /// current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the 
        /// command does not require data to be passed,
        /// this object can be set to null.</param>
        /// <returns>True if the command can be executed.</returns>
        public bool CanExecute(object parameter)
        {
            return this.IsEnabled;
        }

        /// <summary>
        /// This is the event that WPF's command architecture listens to so 
        /// it knows when to update the UI on command enable/disable.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        /// <summary>
        /// Public visibility of the isEnabled flag - note that when it is 
        /// set, need to raise the event so that WPF knows to update 
        /// any UI that uses this command.
        /// </summary>
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set
            {
                this.isEnabled = value;
                this.OnCanExecuteChanged();
            }
        }

        /// <summary>
        /// Simple event propagation that makes sure someone is 
        /// listening to the event before raising it.
        /// </summary>
        private void OnCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
