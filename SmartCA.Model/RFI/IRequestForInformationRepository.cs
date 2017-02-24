using System;
using System.Collections.Generic;
using SmartCA.Infrastructure.RepositoryFramework;
using SmartCA.Model.Projects;
using SmartCA.Model.NumberedProjectChildren;

namespace SmartCA.Model.RFI
{
    public interface IRequestForInformationRepository
        : INumberedProjectChildRepository<RequestForInformation>
    {
    }
}
