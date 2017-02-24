using System;
using System.Configuration;
using SmartCA.Infrastructure.RepositoryFramework.Configuration;

namespace SmartCA.Infrastructure.Membership
{
    public static class ProviderFactory
    {
        private static object provider;

        public static T GetProvider<T>() where T : class
        {
            // See if the provider instance was already created
            if (ProviderFactory.provider == null)
            {
                // It was not created, so build it now
                RepositorySettings settings =
                    (RepositorySettings)ConfigurationManager.GetSection(
                    RepositoryMappingConstants.RepositoryMappingsConfigurationSectionName);

                // Get the type to be created
                string interfaceTypeName = typeof(T).Name;
                Type repositoryType =
                    Type.GetType(
                    settings.RepositoryMappings[interfaceTypeName].RepositoryFullTypeName);

                // Create the provider
                ProviderFactory.provider =
                    Activator.CreateInstance(repositoryType);
            }

            return ProviderFactory.provider as T;
        }
    }
}
