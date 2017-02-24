using System;
using System.Windows;
using SmartCA.Presentation.ViewModels;
using SmartCA.Infrastructure.UI;

namespace SmartCA.Presentation.Views
{
	public partial class ProjectInformationView : Window, IView
    {
        public ProjectInformationView()
		{
			this.InitializeComponent();
            this.DataContext = new ProjectInformationViewModel(this);
		}
    }
}