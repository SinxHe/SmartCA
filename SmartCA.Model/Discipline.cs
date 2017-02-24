using System;

namespace SmartCA.Model
{
    public class Discipline
    {
        object key;
        private string name;
        private string description;

        public Discipline(object key, string name, 
            string description)
        {
            this.key = key;
            this.name = name;
            this.description = description;
        }

        public object Key
        {
            get { return this.key; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public string Description
        {
            get { return this.description; }
        }

        public override bool Equals(object obj)
        {
            return obj != null
                && obj.GetType() == typeof(Discipline)
                && this == (Discipline)obj;
        }

        public static bool operator ==(Discipline one, Discipline other)
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

            if (one.Key != other.Key)
            {
                return false;
            }

            return true;
        }

        public static bool operator !=(Discipline one, Discipline other)
        {
            return !(one == other);
        }

        public override int GetHashCode()
        {
            return this.key.GetHashCode();
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
