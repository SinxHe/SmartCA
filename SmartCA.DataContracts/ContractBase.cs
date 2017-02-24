using System;

namespace SmartCA.DataContracts
{
    [Serializable]
    public abstract class ContractBase
    {
        private object key;

        /// <summary>
        /// An <see cref="System.Object"/> that represents the 
        /// primary identifier value for the class.
        /// </summary>
        public object Key
        {
            get { return this.key; }
            set { this.key = value; }
        }
    }
}
