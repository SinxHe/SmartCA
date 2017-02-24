using System;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class RoleContract
    {
        private string name;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
    }
}
