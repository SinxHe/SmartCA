using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace SmartCA.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ChangeOrderView.xaml
    /// </summary>

    public partial class ChangeOrderView : Window, IView
    {
        public ChangeOrderView()
        {
            InitializeComponent();
            this.DataContext = new ChangeOrderViewModel(this);
        }
    }
}
