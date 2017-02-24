using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SmartCA.Infrastructure.UI;
using SmartCA.Presentation.ViewModels;
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SmartCA.Presentation.Views
{
    /// <summary>
    /// Interaction logic for CompanyView.xaml
    /// </summary>

    public partial class CompanyView : Window, IView
    {
        public CompanyView()
        {
            InitializeComponent();
            this.DataContext = new CompanyViewModel(this);
        }
    }
}
