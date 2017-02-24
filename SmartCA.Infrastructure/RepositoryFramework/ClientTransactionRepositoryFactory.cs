using System;
using SmartCA.Infrastructure.Transactions;
using SmartCA.Infrastructure.RepositoryFramework.Configuration;
using System.Configuration;

namespace SmartCA.Infrastructure.RepositoryFramework
{
    public static class ClientTransactionRepositoryFactory
    {
        private static IClientTransactionRepository transactionRepository;

        public static IClientTransactionRepository GetTransactionRepository()
        {
            // See if the ITransactionRepository instance was already created
            if (ClientTransactionRepositoryFactory.transactionRepository == null)
            {
                // It was not created, so build it now
                RepositorySettings settings = 
                    (RepositorySettings)ConfigurationManager.GetSection(
                    RepositoryMappingConstants.RepositoryMappingsConfigurationSectionName);

                // Get the type to be created
                Type repositoryType = 
                    Type.GetType(
                    settings.RepositoryMappings["IClientTransactionRepository"].RepositoryFullTypeName);

                // Create the repository, and cast it to the 
                // ITransactionRepository interface
                ClientTransactionRepositoryFactory.transactionRepository = 
                    Activator.CreateInstance(repositoryType) 
                    as IClientTransactionRepository;

            }

            return ClientTransactionRepositoryFactory.transactionRepository;
        }
    }
}
