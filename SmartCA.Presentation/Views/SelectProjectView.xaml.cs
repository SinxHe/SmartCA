using System;
using System.Windows;
using System.Windows.Controls;
using SmartCA.Presentation.ViewModels;
using SmartCA.Infrastructure.UI;

namespace SmartCA.Presentation.Views
{
    public partial class SelectProjectView : Window, IView
    {
        public SelectProjectView()
        {
            this.InitializeComponent();
            this.DataContext = new SelectProjectViewModel(this);
        }
    }
}
