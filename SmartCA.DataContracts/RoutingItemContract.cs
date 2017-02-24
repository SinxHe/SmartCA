using System;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class RoutingItemContract
    {
        private object key;
        private DisciplineContract discipline;
        private int routingOrder;
        private ProjectContactContract recipient;
        private DateTime dateSent;
        private DateTime? dateReturned;
        
        public RoutingItemContract()
        {
            this.key = null;
            this.discipline = null;
            this.routingOrder = 0;
            this.recipient = null;
            this.dateReturned = null;
        }

        public object Key
        {
            get { return this.key; }
            set { this.key = value; }
        }

        public DisciplineContract Discipline
        {
            get { return this.discipline; }
            set { this.discipline = value; }
        }

        public int RoutingOrder
        {
            get { return this.routingOrder; }
            set { this.routingOrder = value; }
        }

        public ProjectContactContract Recipient
        {
            get { return this.recipient; }
            set { this.recipient = value; }
        }

        public DateTime DateSent
        {
            get { return this.dateSent; }
            set { this.dateSent = value; }
        }

        public DateTime? DateReturned
        {
            get { return this.dateReturned; }
            set { this.dateReturned = value; }
        }
    }
}
