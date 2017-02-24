using System;

namespace SmartCA.Model.Submittals
{
    public class SpecificationSection
    {
        private int key;
        private string number;
        private string title;
        private string description;

        public SpecificationSection(int key, string number, 
            string title, string description)
        {
            this.key = key;
            this.number = number;
            this.title = title;
            this.description = description;
        }

        public int Key
        {
            get { return this.key; }
        }

        public string Number
        {
            get { return this.number; }
        }

        public string Title
        {
            get { return this.title; }
        }

        public string Description
        {
            get { return this.description; }
        }

        public override string ToString()
        {
            return this.Title;
        }

        public override bool Equals(object obj)
        {
            return obj != null
                && obj.GetType() == typeof(SpecificationSection)
                && this == (SpecificationSection)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(SpecificationSection one, SpecificationSection other)
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

            if (one.key != other.key)
            {
                return false;
            }

            return true;
        }

        public static bool operator !=(SpecificationSection one, SpecificationSection other)
        {
            return !(one == other);
        }
    }
}
