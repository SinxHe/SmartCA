using SmartCA.Model.Projects;
using SmartCA.Model.Transmittals;

namespace SmartCA.Presentation.ViewModels
{
    public class MutableCopyTo
    {
        private ProjectContact projectContact;
        private string notes;

        public MutableCopyTo() 
            : this(null)
        {
        }

        public MutableCopyTo(CopyTo copyTo)
        {
            if (copyTo != null)
            {
                this.projectContact = copyTo.Contact;
                this.notes = copyTo.Notes;
            }
            else
            {
                this.projectContact = null;
                this.notes = string.Empty;
            }
        }

        public ProjectContact ProjectContact
        {
            get { return this.projectContact; }
            set { this.projectContact = value; }
        }

        public string Notes
        {
            get { return this.notes; }
            set { this.notes = value; }
        }

        public CopyTo ToCopyTo()
        {
            return new CopyTo(this.projectContact, this.notes);
        }

        public override bool Equals(object obj)
        {
            return obj != null
                && obj.GetType() == typeof(MutableCopyTo)
                && this == (MutableCopyTo)obj;
        }

        public override int GetHashCode()
        {
            int projectContactHashCode = 0;
            if (this.projectContact != null)
            {
                projectContactHashCode = this.projectContact.GetHashCode();
            }
            return projectContactHashCode ^ this.notes.GetHashCode();
        }

        public static bool operator ==(MutableCopyTo one, MutableCopyTo other)
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

            if (one.ProjectContact != other.ProjectContact
                || one.Notes != other.Notes)
            {
                return false;
            }

            return true;
        }

        public static bool operator !=(MutableCopyTo one, MutableCopyTo other)
        {
            return !(one == other);
        }

        public override string ToString()
        {
            return this.ToCopyTo().ToString();
        }
    }
}
