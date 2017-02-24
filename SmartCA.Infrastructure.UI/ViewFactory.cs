using System;

namespace SmartCA.Infrastructure.UI
{
    public static class ViewFactory
    {
        public static IView GetView(string name)
        {
            string typeName = string.Empty;
            // TODO:  make this be configuration-based
            switch (name)
            {
                case "ProjectContactView":
                    typeName = "SmartCA.Presentation.Views.ProjectContactView, SmartCA.Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
                    break;
                default:
                    break;
            }
            return Activator.CreateInstance(Type.GetType(typeName)) as IView;
        }
    }
}
