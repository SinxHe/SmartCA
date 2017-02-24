using System;
using System.Windows;
using SmartCA.Presentation.ViewModels;
using SmartCA.Infrastructure.UI;

namespace SmartCA.Presentation.Views
{
    /// <summary>
    /// Interaction logic for SubmittalView.xaml
    /// </summary>
    public partial class SubmittalView : Window, IView
    {
        public SubmittalView()
        {
            InitializeComponent();
            this.DataContext = new SubmittalViewModel(this);
        }
    }
}
