using System;

namespace SmartCA.Model.Transmittals
{
    [Flags]
    public enum Delivery
    {
        None = 0,
        Fax = 1,
        Overnight = 2,
        Mail = 4,
        Hand = 8,
        Other = 16
    }
}
