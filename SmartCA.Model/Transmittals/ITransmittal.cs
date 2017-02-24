using System;
using System.Collections.Generic;
using SmartCA.Model;

namespace SmartCA.Model.Transmittals
{
    public interface ITransmittal
    {
        object ProjectKey { get; }
        DateTime TransmittalDate { get; set; }
        int TotalPages { get; set; }
        Delivery DeliveryMethod { get; set; }
        string OtherDeliveryMethod { get; set; }
        string PhaseNumber { get; set; }
        bool Reimbursable { get; set; }
        bool Final { get; set; }
        IList<CopyTo> CopyToList { get; }
        string TransmittalRemarks { get; set; }
    }
}
