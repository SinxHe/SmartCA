using System;
using SmartCA.Model.Projects;

namespace SmartCA.Model.Transmittals
{
    public class CopyTo
    {
        private ProjectContact contact;
        private string notes;

        public CopyTo(ProjectContact contact, string notes)
        {
            this.contact = contact;
            this.notes = notes;
        }

        public ProjectContact Contact
        {
            get { return this.contact; }
        }

        public string Notes
        {
            get { return this.notes; }
        }

        public override bool Equals(object obj)
        {
            return obj != null
                && obj.GetType() == typeof(CopyTo)
                && this == (CopyTo)obj;
        }

        public static bool operator ==(CopyTo one, CopyTo other)
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

            if (one.Contact != other.Contact
                || one.Notes != other.Notes)
            {
                return false;
            }

            return true;
        }

        public static bool operator !=(CopyTo one, CopyTo other)
        {
            return !(one == other);
        }

        public override int GetHashCode()
        {
            return this.contact.GetHashCode()
                ^ this.notes.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", 
                this.contact.Contact.LastName, this.notes);
        }
    }
}
