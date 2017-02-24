
namespace SmartCA.Model.Membership
{
    public class Role
    {
        private string name;

        public Role(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return this.name; }
        }
    }
}
