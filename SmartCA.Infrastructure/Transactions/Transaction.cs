using System;
using SmartCA.Infrastructure.DomainBase;

namespace SmartCA.Infrastructure.Transactions
{
    public abstract class Transaction : IEntity
    {
        private object key;
        private TransactionType type;

        protected Transaction(object key, TransactionType type)
        {
            this.key = key;
            if (this.key == null)
            {
                this.key = Guid.NewGuid();
            }
            this.type = type;
        }

        public TransactionType Type
        {
            get { return this.type; }
        }

        #region IEntity Members

        public object Key
        {
            get { return this.key; }
        }

        #endregion

        #region Equality Tests

        /// <summary>
        /// Determines whether the specified transaction is equal to the 
        /// current instance.
        /// </summary>
        /// <param name="entity">An <see cref="System.Object"/> that 
        /// will be compared to the current instance.</param>
        /// <returns>True if the passed in entity is equal to the 
        /// current instance.</returns>
        public override bool Equals(object transaction)
        {
            return transaction != null
                && transaction is Transaction
                && this == (Transaction)transaction;
        }

        /// <summary>
        /// Operator overload for determining equality.
        /// </summary>
        /// <param name="base1">The first instance of an 
        /// <see cref="Transaction"/>.</param>
        /// <param name="base2">The second instance of an 
        /// <see cref="Transaction"/>.</param>
        /// <returns>True if equal.</returns>
        public static bool operator ==(Transaction base1,
            Transaction base2)
        {
            // check for both null (cast to object or recursive loop)
            if ((object)base1 == null && (object)base2 == null)
            {
                return true;
            }

            // check for either of them == to null
            if ((object)base1 == null || (object)base2 == null)
            {
                return false;
            }

            if (base1.Key != base2.Key)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Operator overload for determining inequality.
        /// </summary>
        /// <param name="base1">The first instance of an 
        /// <see cref="Transaction"/>.</param>
        /// <param name="base2">The second instance of an 
        /// <see cref="Transaction"/>.</param>
        /// <returns>True if not equal.</returns>
        public static bool operator !=(Transaction base1,
            Transaction base2)
        {
            return (!(base1 == base2));
        }

        /// <summary>
        /// Serves as a hash function for this type.
        /// </summary>
        /// <returns>A hash code for the current Key 
        /// property.</returns>
        public override int GetHashCode()
        {
            return this.key.GetHashCode();
        }

        #endregion
    }
}
