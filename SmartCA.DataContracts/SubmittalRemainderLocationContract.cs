using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCA.DataContracts
{
    [Flags]
    public enum SubmittalRemainderLocationContract
    {
        None,
        RollDrawings,
        FilingCabinet,
        FilingCabinetUnderSubmittalNumber,
        Other
    }
}
