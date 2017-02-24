using System;
using System.Configuration;

namespace SmartCA.Infrastructure.EntityFactoryFramework.Configuration
{
    public class EntitySettings : ConfigurationSection
    {
        [ConfigurationProperty(EntityMappingConstants.ConfigurationPropertyName, 
            IsDefaultCollection = true)]
        public EntityMappingCollection EntityMappings
        {
            get { return (EntityMappingCollection)base[EntityMappingConstants.ConfigurationPropertyName]; }
        }
    }
}
