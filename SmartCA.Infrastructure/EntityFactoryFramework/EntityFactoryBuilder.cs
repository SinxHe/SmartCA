using System;
using System.Collections.Generic;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Infrastructure.EntityFactoryFramework.Configuration;
using System.Configuration;

namespace SmartCA.Infrastructure.EntityFactoryFramework
{
    public static class EntityFactoryBuilder
    {
        // Dictionary used for caching purposes
        private static Dictionary<string, object> factories = 
            new Dictionary<string, object>();

        public static IEntityFactory<T> BuildFactory<T>() where T : IEntity
        {
            IEntityFactory<T> factory = null;

            // Get the key from the Generic parameter passed in
            string key = typeof(T).Name;

            // See if the factory is in the cache
            if (EntityFactoryBuilder.factories.ContainsKey(key))
            {
                // It was there, so retrieve it from the cache
                factory = EntityFactoryBuilder.factories[key] as IEntityFactory<T>;
            }
            else
            {
                // Create the factory

                // Get the entityMappingsConfiguration config section
                EntitySettings settings = (EntitySettings)ConfigurationManager.GetSection(EntityMappingConstants.EntityMappingsConfigurationSectionName);

                // Get the type to be created using reflection
                Type entityFactoryType = Type.GetType(settings.EntityMappings[key].EntityFactoryFullTypeName);

                // Create the factory using reflection
                factory = Activator.CreateInstance(entityFactoryType) as IEntityFactory<T>;

                // Put the newly created factory in the cache
                EntityFactoryBuilder.factories[key] = factory;
            }

            // Return the factory
            return factory;
        }
    }
}
