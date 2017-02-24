using System.Data;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Model;
using SmartCA.Model.Projects;
using SmartCA.Model.Transmittals;

namespace SmartCA.Infrastructure.Repositories
{
    internal static class TransmittalFactory
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string ProjectContactId = "ProjectContactID";
            public const string ItemStatusId = "ItemStatusID";
            public const string ItemStatus = "Status";
            public const string Notes = "Notes";
            public const string Description = "Description";
            public const string Status = "Status";
            public const string DisciplineId = "DisciplineID";
            public const string DisciplineName = "DisciplineName";
            public const string RoutingItemId = "RoutingItemID";
            public const string RoutingOrder = "RoutingOrder";
            public const string DateSent = "DateSent";
            public const string DateReturned = "DateReturned";
            public const string DeliveryMethod = "DeliveryMethod";
            public const string Final = "Final";
            public const string OtherDeliveryMethod = "OtherDeliveryMethod";
            public const string PhaseNumber = "PhaseNumber";
            public const string Reimbursable = "Reimbursable";
            public const string TotalPages = "TotalPages";
            public const string TransmittalDate = "TransmittalDate";
            public const string TransmittalRemarks = "TransmittalRemarks";
        }

        #endregion

        internal static ItemStatus BuildItemStatus(IDataReader reader)
        {
            return new ItemStatus(DataHelper.GetInteger(reader[FieldNames.ItemStatusId]),
                                   reader[FieldNames.ItemStatus].ToString().Trim());
        }

        internal static CopyTo BuildCopyTo(object projectKey, IDataReader reader)
        {
            ProjectContact contact = ProjectService.GetProjectContact(projectKey,
                reader[FieldNames.ProjectContactId]);
            return new CopyTo(contact, reader[FieldNames.Notes].ToString());
        }

        internal static Discipline BuildDiscipline(IDataReader reader)
        {
            return new Discipline(reader[FieldNames.DisciplineId],
                   reader[FieldNames.DisciplineName].ToString(),
                   reader[FieldNames.Description].ToString());
        }

        internal static RoutingItem BuildRoutingItem(object projectKey, IDataReader reader)
        {
            ProjectContact contact = ProjectService.GetProjectContact(projectKey,
                reader[FieldNames.ProjectContactId]);
            return new RoutingItem(reader[FieldNames.RoutingItemId],
                       TransmittalFactory.BuildDiscipline(reader),
                       DataHelper.GetInteger(reader[FieldNames.RoutingOrder]),
                       contact,
                       DataHelper.GetDateTime(reader[FieldNames.DateSent]),
                       DataHelper.GetNullableDateTime(reader[FieldNames.DateReturned]));
        }
    }
}
