using System;
using System.Text;

namespace SmartCA.Model.Addresses
{
    /// <summary>
    /// This is an immutable Value class.
    /// </summary>
    public sealed class Address
    {
        private string street;
        private string city;
        private string state;
        private string postalCode;

        public Address(string street, string city, string state, string postalCode) 
            : this(street, city, state, postalCode, true)
        {
        }

        private Address(string street, string city, string state, string postalCode, 
            bool validate)
        {
            this.street = street;
            this.city = city;
            this.state = state;
            this.postalCode = postalCode;
            if (validate)
            {
                this.Validate();
            }
        }

        public string Street
        {
            get { return this.street; }
        }

        public string City
        {
            get { return this.city; }
        }

        public string State
        {
            get { return this.state; }
        }

        public string PostalCode
        {
            get { return this.postalCode; }
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(this.street) ||
                string.IsNullOrEmpty(this.city) ||
                string.IsNullOrEmpty(this.state) ||
                string.IsNullOrEmpty(this.postalCode))
            {
                throw new InvalidOperationException("Invalid address.");
            }
        }

        public override bool Equals(object obj)
        {
            return obj != null 
                && obj.GetType() == typeof(Address) 
                && this == (Address)obj;
        }

        public override int GetHashCode()
        {
            return this.street.GetHashCode() 
                ^ this.city.GetHashCode() 
                ^ this.state.GetHashCode() 
                ^ this.postalCode.GetHashCode();
        }

        public static bool operator ==(Address one, Address other)
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

            if (one.street != other.street
                || one.city != other.city
                || one.state != other.state
                || one.postalCode != other.postalCode)
            {
                return false;
            }

            return true;
        }

        public static bool operator !=(Address one, Address other)
        {
            return !(one == other);
        }

        public static Address Empty()
        {
            return new Address(string.Empty, string.Empty, 
                       string.Empty, string.Empty, false);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(300);
            builder.Append(this.street);
            builder.Append("\r\n");
            builder.Append(this.city);
            builder.Append(", ");
            builder.Append(this.state);
            builder.Append("  ");
            builder.Append(this.postalCode);
            return builder.ToString();
        }
    }
}
