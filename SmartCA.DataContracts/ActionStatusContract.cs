using System;

namespace SmartCA.DataContracts
{
    public enum ActionStatusContract
    {
        Accepted,
        AgencyApproved,
        MakeCorrectionsNoted,
        NoExceptionTaken,
        ReceiptAcknowledgedNoActionTaken,
        Rejected,
        ReturnedNoComment,
        ReviseResubmit,
        SubmitSpecificItem,
    }
}
