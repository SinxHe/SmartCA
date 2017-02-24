using SmartCA.Model.Addresses;

namespace SmartCA.Presentation.ViewModels
{
    public class MutableAddress
    {
        private string street;
        private string city;
        private string state;
        private string postalCode;

        public MutableAddress() 
            : this(null)
        {
        }

        public MutableAddress(Address address)
        {
            if (address != null)
            {
                this.street = address.Street;
                this.city = address.City;
                this.state = address.State;
                this.postalCode = address.PostalCode;
            }
            else
            {
                this.street = string.Empty;
                this.city = string.Empty;
                this.state = string.Empty;
                this.postalCode = string.Empty;
            }
        }

        public string Street
        {
            get { return this.street; }
            set { this.street = value; }
        }

        public string City
        {
            get { return this.city; }
            set { this.city = value; }
        }

        public string State
        {
            get { return this.state; }
            set { this.state = value; }
        }

        public string PostalCode
        {
            get { return this.postalCode; }
            set { this.postalCode = value; }
        }

        public Address ToAddress()
        {
            return new Address(this.street, this.city, 
                       this.state, this.postalCode);
        }

        public override bool Equals(object obj)
        {
            return obj != null
                && obj.GetType() == typeof(MutableAddress)
                && this == (MutableAddress)obj;
        }

        public override int GetHashCode()
        {
            return this.street.GetHashCode()
                ^ this.city.GetHashCode()
                ^ this.state.GetHashCode()
                ^ this.postalCode.GetHashCode();
        }

        public static bool operator ==(MutableAddress one, MutableAddress other)
        {
            // check for both null (cast to object to avoid recursive loop)
            if ((object)one == null && (object)other == null)
            {
                return true;
            }

            // check for either of them equal to null
            if ((object)one == null || (object)other == null)
            {
                return false;
            }

            if (one.Street != other.Street
                || one.City != other.City
                || one.State != other.State
                || one.PostalCode != other.PostalCode)
            {
                return false;
            }

            return true;
        }

        public static bool operator !=(MutableAddress one, MutableAddress other)
        {
            return !(one == other);
        }

        public override string ToString()
        {
            return this.ToAddress().ToString();
        }
    }
}
