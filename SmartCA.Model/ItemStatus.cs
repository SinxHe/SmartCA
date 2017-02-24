using System;

namespace SmartCA.Model
{
    public class ItemStatus
    {
        private int id;
        private string status;

        public ItemStatus(int id, string status)
        {
            this.id = id;
            this.status = status;
        }

        public int Id
        {
            get { return this.id; }
        }

        public string Status
        {
            get { return this.status; }
        }

        public override string ToString()
        {
            return this.Status;
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj != null
                && obj.GetType() == typeof(ItemStatus)
                && this == (ItemStatus)obj;
        }

        public static bool operator ==(ItemStatus one, ItemStatus other)
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

            if (one.Id != other.Id)
            {
                return false;
            }

            return true;
        }

        public static bool operator !=(ItemStatus one, ItemStatus other)
        {
            return !(one == other);
        }
    }
}