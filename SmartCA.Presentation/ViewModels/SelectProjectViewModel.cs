using System;
using System.Windows.Data;
using SmartCA.Application;
using SmartCA.Infrastructure.UI;
using SmartCA.Model.Projects;

namespace SmartCA.Presentation.ViewModels
{
    public class SelectProjectViewModel : ViewModel
    {
        private CollectionView projects;
        private DelegateCommand selectCommand;
        
        public SelectProjectViewModel() 
            : this(null)
        {
        }

        public SelectProjectViewModel(IView view) 
            : base(view)
        {
            this.projects = new CollectionView(ProjectService.GetAllProjects());
            this.selectCommand = new DelegateCommand(this.SelectCommandHandler);
        }

        public CollectionView Projects
        {
            get { return this.projects; }
        }

        public DelegateCommand SelectCommand
        {
            get { return this.selectCommand; }
        }

        private void SelectCommandHandler(object sender, EventArgs e)
        {
            Project project = this.projects.CurrentItem as Project;
            UserSession.CurrentProject = project;
            this.CloseView();
        }
    }
}
