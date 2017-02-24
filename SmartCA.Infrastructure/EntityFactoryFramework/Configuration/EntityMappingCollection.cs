using System;
using System.Configuration;

namespace SmartCA.Infrastructure.EntityFactoryFramework.Configuration
{
    public sealed class EntityMappingCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EntityMappingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EntityMappingElement)element).EntityShortTypeName;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return EntityMappingConstants.ConfigurationElementName; }
        }

        public EntityMappingElement this[int index]
        {
            get { return (EntityMappingElement)this.BaseGet(index); }
            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new EntityMappingElement this[string entityShortTypeName]
        {
            get { return (EntityMappingElement)this.BaseGet(entityShortTypeName); }
        }

        public bool ContainsKey(string keyName)
        {
            bool result = false;
            object[] keys = this.BaseGetAllKeys();
            foreach (object key in keys)
            {
                if ((string)key == keyName)
                {
                    result = true;
                    break;

                }
            }
            return result;
        }
    }
}
