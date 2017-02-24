using System;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Infrastructure.RepositoryFramework;
using System.Collections.Generic;
using System.Linq;
using SmartCA.Model.Projects;

namespace SmartCA.Model.NumberedProjectChildren
{
    public static class NumberedProjectChildFactory
    {
        public static T CreateNumberedProjectChild<T>(Project project) 
            where T : EntityBase, IAggregateRoot, INumberedProjectChild
        {
            // Initialize the NumberedProjectChild return value
            T newNumberedProjectChild = default(T);

            // Get the correct repository using the Repository Factory
            INumberedProjectChildRepository<T> repository = 
                RepositoryFactory.GetRepository
                <INumberedProjectChildRepository<T>, T>();

            // Get all of the items in the Aggregate from the FindBy method
            IList<T> numberedProjectChildren = repository.FindBy(project);

            // Use LINQ to get the last numbered item in the list
            // and increment it by 1
            int newNumber = numberedProjectChildren.Last().Number + 1;

            // Create the instance, passing in the projectKey value as well 
            // as the new number to the constructor of the INumberedProjectChild 
            // instance, and then casting it to the correct type (T)
            newNumberedProjectChild = Activator.CreateInstance(typeof(T), 
                new object[] { project.Key, newNumber }) as T;

            // Return the newly initialized object
            return newNumberedProjectChild;
        }
    }
}
