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
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using SmartCA.Presentation.Views;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using SmartCA.Infrastructure.UI;

namespace SmartCA.Presentation
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, EventArgs e)
        {
            this.SelectProject();
        }

        private void selectProjectButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.SelectProject();
        }

        private void SelectProject()
        {
            IView view = new SelectProjectView();
            view.Show();
        }

        private void projectInformationButton_Clicked(object sender, RoutedEventArgs e)
        {
            IView view = new ProjectInformationView();
            view.Show();
        }

        private void companiesButton_Clicked(object sender, RoutedEventArgs e)
        {
            IView view = new CompanyView();
            view.Show();
        }

        private void submittalsButton_Clicked(object sender, RoutedEventArgs e)
        {
            IView view = new SubmittalView();
            view.Show();
        }

        private void rfiButton_Clicked(object sender, RoutedEventArgs e)
        {
            IView view = new RequestForInformationView();
            view.Show();
        }

        private void proposalRequestsButton_Clicked(object sender, RoutedEventArgs e)
        {
            IView view = new ProposalRequestView();
            view.Show();
        }

        private void changeOrdersButton_Clicked(object sender, RoutedEventArgs e)
        {
            IView view = new ChangeOrderView();
            view.Show();
        }

        private void constructionChangeDirectivesButton_Clicked(object sender, RoutedEventArgs e)
        {
            IView view = new ConstructionChangeDirectiveView();
            view.Show();
        }
    }
}
