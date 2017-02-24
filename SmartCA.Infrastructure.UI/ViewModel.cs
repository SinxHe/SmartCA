using System;

namespace SmartCA.Infrastructure.UI
{
    public abstract class ViewModel
    {
        private IView view;
        private DelegateCommand cancelCommand;

        protected ViewModel()
            : this(null)
        {
        }

        protected ViewModel(IView view)
        {
            this.view = view;
            this.cancelCommand = new DelegateCommand(this.CancelCommandHandler);
        }

        public DelegateCommand CancelCommand
        {
            get { return this.cancelCommand; }
        }

        protected virtual void CancelCommandHandler(object sender, EventArgs e)
        {
            this.CloseView();
        }

        protected void CloseView()
        {
            if (this.view != null)
            {
                this.view.Close();
            }
        }
    }
}
