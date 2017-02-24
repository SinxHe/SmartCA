using System;

namespace SmartCA.Model.Submittals
{
    [Flags]
    public enum SubmittalRemainderLocation
    {
        None,
        RollDrawings,
        FilingCabinet,
        FilingCabinetUnderSubmittalNumber,
        Other
    }
}