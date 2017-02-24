using System;
using System.Text;

namespace SmartCA.Model.NumberedProjectChildren
{
    public static class NumberedProjectChildValidator
    {
        /// <summary>
        /// This method throws an exception if the initial state is not valid.
        /// </summary>
        /// <param name="child">The Entity instance, which must implement the 
        /// INumberedProjectChild interface.</param>
        /// <param name="entityFriendlyName">The friendly name of the Entity, 
        /// such as "Change Order".</param>
        public static void ValidateInitialState(INumberedProjectChild child, 
            string entityFriendlyName)
        {
            if (child.Key == null &&
                (child.Number < 1 || child.ProjectKey == null))
            {
                StringBuilder builder = new StringBuilder(100);
                builder.Append(string.Format("Invalid {0}.  ", 
                    entityFriendlyName));
                builder.Append(string.Format("The {0} must have ", 
                    entityFriendlyName));
                builder.Append(string.Format("a valid {0} number ", 
                    entityFriendlyName));
                builder.Append("and be associated with a Project.");
                throw new InvalidOperationException(builder.ToString());
            }
        }
    }
}
