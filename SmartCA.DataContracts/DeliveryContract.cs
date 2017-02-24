using System;

namespace SmartCA.DataContracts
{
    [Flags]
    public enum DeliveryContract
    {
        None = 0,
        Fax = 1,
        Overnight = 2,
        Mail = 4,
        Hand = 8,
        Other = 16
    }
}
