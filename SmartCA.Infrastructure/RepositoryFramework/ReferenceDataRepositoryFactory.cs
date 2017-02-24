using System;
using SmartCA.Infrastructure.RepositoryFramework.Configuration;
using System.Configuration;
using SmartCA.Infrastructure.ReferenceData;

namespace SmartCA.Infrastructure.RepositoryFramework
{
    public static class ReferenceDataRepositoryFactory
    {
        private static IReferenceDataRepository referenceDataRepository;

        public static IReferenceDataRepository GetReferenceDataRepository()
        {
            // See if the IReferenceDataRepository instance was already created
            if (ReferenceDataRepositoryFactory.referenceDataRepository == null)
            {
                // It was not created, so build it now
                RepositorySettings settings = 
                    (RepositorySettings)ConfigurationManager.GetSection(
                    RepositoryMappingConstants.RepositoryMappingsConfigurationSectionName);

                // Get the type to be created
                Type repositoryType = 
                    Type.GetType(
                    settings.RepositoryMappings["IReferenceDataRepository"].RepositoryFullTypeName);

                // Create the repository, and cast it to the 
                // IReferenceDataRepository interface
                ReferenceDataRepositoryFactory.referenceDataRepository =
                    Activator.CreateInstance(repositoryType) as IReferenceDataRepository;

            }

            return ReferenceDataRepositoryFactory.referenceDataRepository;
        }
    }
}
