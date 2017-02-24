using System;
using System.Collections.Generic;
using SmartCA.Model.Projects;

namespace SmartCA.Model.Transmittals
{
    public class RoutingItem
    {
        private object key;
        private Discipline discipline;
        private int routingOrder;
        private ProjectContact recipient;
        private DateTime dateSent;
        private DateTime? dateReturned;
        private int daysLapsed;

        public RoutingItem(object key, Discipline discipline, 
            int routingOrder, ProjectContact recipient, 
            DateTime dateSent) 
            : this(key, discipline, 
                routingOrder, recipient, dateSent, null)
        {
        }

        public RoutingItem(object key, Discipline discipline,
            int routingOrder, ProjectContact recipient,
            DateTime dateSent, DateTime? dateReturned)
        {
            this.key = key;
            this.discipline = discipline;
            this.routingOrder = routingOrder;
            this.recipient = recipient;
            this.dateSent = dateSent;
            this.DateReturned = dateReturned;
        }

        public object Key
        {
            get { return this.key; }
        }

        public Discipline Discipline
        {
            get { return this.discipline; }
            set { this.discipline = value;}
        }

        public int RoutingOrder
        {
            get { return this.routingOrder; }
            set { this.routingOrder = value; }
        }

        public ProjectContact Recipient
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
            set 
            {
                if (value != this.dateReturned && value.HasValue)
                {
                    this.dateReturned = value;
                    this.CalculateDaysLapsed();
                }
            }
        }

        public int DaysLapsed
        {
            get { return this.daysLapsed; }
        }

        private void CalculateDaysLapsed()
        {
            if (this.dateReturned.HasValue && 
                this.dateReturned.Value > this.dateSent)
            {
                this.daysLapsed = 
                    this.dateReturned.Value.Subtract(this.dateSent).Days;
            }
        }
    }
}