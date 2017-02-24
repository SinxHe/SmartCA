using System;
using System.Reflection;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Model;
using SmartCA.Model.Addresses;
using SmartCA.Model.ChangeOrders;
using SmartCA.Model.Companies;
using SmartCA.Model.ConstructionChangeDirectives;
using SmartCA.Model.Contacts;
using SmartCA.Model.Employees;
using SmartCA.Model.Membership;
using SmartCA.Model.Projects;
using SmartCA.Model.ProposalRequests;
using SmartCA.Model.RFI;
using SmartCA.Model.Submittals;
using SmartCA.Model.Transmittals;

namespace SmartCA.DataContracts.Helpers
{
    public static class Converter
    {
        public static object ToContract(IEntity entity)
        {
            // Do reflection to call the right method here
            string methodName = string.Format("To{0}Contract", entity.GetType().Name);
            MethodInfo method = typeof(Converter).GetMethod(methodName);
            return method.Invoke(null, new object[] { entity });
        }

        public static IEntity ToEntity(ContractBase contract)
        {
            // Do reflection to call the right method here
            string methodName = string.Format("To{0}", 
                contract.GetType().Name.Replace("Contract", ""));
            MethodInfo method = typeof(Converter).GetMethod(methodName);
            return method.Invoke(null, new object[] { contract }) as IEntity;
        }

        public static CompanyContract ToCompanyContract(Company company)
        {
            CompanyContract contract = null;
            if (company != null)
            {
                contract = new CompanyContract();
                contract.Key = company.Key;
                contract.Abbreviation = company.Abbreviation;
                foreach (Address address in company.Addresses)
                {
                    contract.Addresses.Add(Converter.ToAddressContract(address));
                }
                contract.FaxNumber = company.FaxNumber;
                contract.HeadquartersAddress =
                    Converter.ToAddressContract(company.HeadquartersAddress);
                contract.Name = company.Name;
                contract.PhoneNumber = company.PhoneNumber;
                contract.Remarks = company.Remarks;
                contract.Url = company.Url;
            }
            return contract;
        }

        public static Company ToCompany(CompanyContract contract)
        {
            Company company = null;
            if (contract != null)
            {
                company = new Company(contract.Key);
                company.Abbreviation = contract.Abbreviation;
                foreach (AddressContract address in contract.Addresses)
                {
                    company.Addresses.Add(Converter.ToAddress(address));
                }
                company.FaxNumber = contract.FaxNumber;
                company.HeadquartersAddress =
                    Converter.ToAddress(contract.HeadquartersAddress);
                company.Name = contract.Name;
                company.PhoneNumber = contract.PhoneNumber;
                company.Remarks = contract.Remarks;
                company.Url = contract.Url;
            }
            return company;
        }

        public static AddressContract ToAddressContract(Address address)
        {
            AddressContract contract = null;
            if (address != null)
            {
                contract = new AddressContract();
                contract.City = address.City;
                contract.PostalCode = address.PostalCode;
                contract.State = address.State;
                contract.Street = address.Street;
            }
            return contract;
        }

        public static Address ToAddress(AddressContract contract)
        {
            Address address = null;
            if (contract != null)
            {
                address = new Address(contract.Street, contract.City,
                              contract.State, contract.PostalCode);
            }
            return address;
        }

        public static ContactContract ToContactContract(Contact contact)
        {
            ContactContract contract = null;
            if (contact != null)
            {
                contract = new ContactContract();
                contract.Key = contact.Key;
                foreach (Address address in contact.Addresses)
                {
                    contract.Addresses.Add(Converter.ToAddressContract(address));
                }
                contract.CurrentCompany = Converter.ToCompanyContract(contact.CurrentCompany);
                contract.Email = contact.Email;
                contract.FaxNumber = contact.FaxNumber;
                contract.JobTitle = contact.JobTitle;
                contract.MobilePhoneNumber = contact.MobilePhoneNumber;
                contract.PhoneNumber = contact.PhoneNumber;
                contract.Remarks = contact.Remarks;
            }
            return contract;
        }

        public static Contact ToContact(ContactContract contract)
        {
            Contact contact = null;
            if (contract != null)
            {
                contact = new Contact(contract.Key, contract.FirstName,
                                       contract.LastName);
                foreach (AddressContract address in contract.Addresses)
                {
                    contact.Addresses.Add(Converter.ToAddress(address));
                }
                contact.CurrentCompany = Converter.ToCompany(contract.CurrentCompany);
                contact.Email = contact.Email;
                contact.FaxNumber = contact.FaxNumber;
                contact.JobTitle = contact.JobTitle;
                contact.MobilePhoneNumber = contact.MobilePhoneNumber;
                contact.PhoneNumber = contact.PhoneNumber;
                contact.Remarks = contact.Remarks;
            }
            return contact;
        }

        public static EmployeeContract ToEmployeeContract(Employee employee)
        {
            EmployeeContract contract = null;
            if (employee != null)
            {
                contract = new EmployeeContract();
                contract.Key = employee.Key;
                contract.FirstName = employee.FirstName;
                contract.Initials = employee.Initials;
                contract.JobTitle = employee.JobTitle;
                contract.LastName = employee.LastName;
            }
            return contract;
        }

        public static Employee ToEmployee(EmployeeContract contract)
        {
            Employee employee = null;
            if (contract != null)
            {
                employee = new Employee(contract.Key, contract.FirstName,
                                       contract.LastName);
                employee.FirstName = contract.FirstName;
                employee.Initials = contract.Initials;
                employee.JobTitle = contract.JobTitle;
                employee.LastName = contract.LastName;
            }
            return employee;
        }

        public static AllowanceContract ToAllowanceContract(Allowance allowance)
        {
            AllowanceContract contract = null;
            if (allowance != null)
            {
                contract = new AllowanceContract();
                contract.Amount = allowance.Amount;
                contract.Title = allowance.Title;
            }
            return contract;
        }

        public static Allowance ToAllowance(AllowanceContract contract)
        {
            return (contract != null ? new Allowance(contract.Title, contract.Amount) : null);
        }

        public static ProjectContactContract ToProjectContactContract(ProjectContact contact)
        {
            ProjectContactContract contract = null;
            if (contact != null)
            {
                contract = new ProjectContactContract();
                contract.Key = contact.Key;
                contract.Contact = Converter.ToContactContract(contact.Contact);
                contract.OnFinalDistributionList = contact.OnFinalDistributionList;
                contract.ProjectKey = contact.Project.Key;
            }
            return contract;
        }

        public static ProjectContact ToProjectContact(ProjectContactContract contract)
        {
            ProjectContact contact = null;
            if (contract != null)
            {
                contact =
                    new ProjectContact(contract.ProjectKey,
                    contract.Key, Converter.ToContact(contract.Contact));
                contact.OnFinalDistributionList = contract.OnFinalDistributionList;
            }
            return contact;
        }

        public static ProjectContractContract ToProjectContractContract(Contract projectContract)
        {
            ProjectContractContract contract = null;
            if (projectContract != null)
            {
                contract = new ProjectContractContract();
                contract.Key = projectContract.Key;
                contract.BidPackageNumber = projectContract.BidPackageNumber;
                contract.ContractAmount = projectContract.ContractAmount;
                contract.ContractDate = projectContract.ContractDate;
                contract.Contractor = Converter.ToCompanyContract(projectContract.Contractor);
                contract.NoticeToProceedDate = projectContract.NoticeToProceedDate;
                contract.ScopeOfWork = projectContract.ScopeOfWork;
            }
            return contract;
        }

        public static Contract ToContract(ProjectContractContract contract)
        {
            Contract projectContract = null;
            if (contract != null)
            {
                projectContract = new Contract();
                projectContract.BidPackageNumber = contract.BidPackageNumber;
                projectContract.ContractAmount = contract.ContractAmount;
                projectContract.ContractDate = contract.ContractDate;
                projectContract.Contractor = Converter.ToCompany(contract.Contractor);
                projectContract.NoticeToProceedDate = contract.NoticeToProceedDate;
                projectContract.ScopeOfWork = contract.ScopeOfWork;
            }
            return projectContract;
        }

        public static MarketSectorContract ToMarketSectorContract(MarketSector sector)
        {
            MarketSectorContract contract = null;
            if (sector != null)
            {
                contract = new MarketSectorContract();
                contract.Key = sector.Key;
                contract.Name = sector.Name;
                foreach (MarketSegment segment in sector.Segments)
                {
                    contract.Segments.Add(Converter.ToMarketSegmentContract(segment));
                }
            }
            return contract;
        }

        public static MarketSector ToMarketSector(MarketSectorContract contract)
        {
            MarketSector sector = null;
            if (contract != null)
            {
                sector = new MarketSector(contract.Key, contract.Name);
                foreach (MarketSegmentContract segment in contract.Segments)
                {
                    sector.Segments.Add(Converter.ToMarketSegment(segment));
                }
            }
            return sector;
        }

        public static MarketSegmentContract ToMarketSegmentContract(MarketSegment segment)
        {
            MarketSegmentContract contract = null;
            if (segment != null)
            {
                contract = new MarketSegmentContract();
                contract.Key = segment.Key;
                contract.Code = segment.Code;
                contract.Name = segment.Name;
                contract.ParentSector = Converter.ToMarketSectorContract(segment.ParentSector);
            }
            return contract;
        }

        public static MarketSegment ToMarketSegment(MarketSegmentContract contract)
        {
            return (contract != null ? new MarketSegment(contract.Key, 
                   Converter.ToMarketSector(contract.ParentSector), 
                   contract.Name, contract.Code) : null);
        }

        public static ProjectContract ToProjectContract(Project project)
        {
            ProjectContract contract = null;
            if (project != null)
            {
                contract = new ProjectContract();
                contract.Key = project.Key;
                contract.ActualCompletionDate = project.ActualCompletionDate;
                contract.Address = Converter.ToAddressContract(project.Address);
                contract.AdjustedCompletionDate = project.AdjustedCompletionDate;
                contract.AdjustedConstructionCost = project.AdjustedConstructionCost;
                contract.AeChangeOrderAmount = project.AeChangeOrderAmount;
                contract.AgencyApplicationNumber = project.AgencyApplicationNumber;
                contract.AgencyFileNumber = project.AgencyFileNumber;
                foreach (Allowance allowance in project.Allowances)
                {
                    contract.Allowances.Add(Converter.ToAllowanceContract(allowance));
                }
                contract.ConstructionAdministrator = Converter.ToEmployeeContract(project.ConstructionAdministrator);
                foreach (ProjectContact contact in project.Contacts)
                {
                    contract.Contacts.Add(Converter.ToProjectContactContract(contact));
                }
                contract.ContingencyAllowanceAmount = project.ContingencyAllowanceAmount;
                contract.ContractDate = project.ContractDate;
                contract.ContractReason = project.ContractReason;
                foreach (Contract projectContract in project.Contracts)
                {
                    contract.Contracts.Add(Converter.ToProjectContractContract(projectContract));
                }
                contract.CurrentCompletionDate = project.CurrentCompletionDate;
                contract.EstimatedCompletionDate = project.EstimatedCompletionDate;
                contract.EstimatedStartDate = project.EstimatedStartDate;
                contract.Name = project.Name;
                contract.Number = project.Number;
                contract.OriginalConstructionCost = project.OriginalConstructionCost;
                contract.Owner = Converter.ToCompanyContract(project.Owner);
                contract.PercentComplete = project.PercentComplete;
                contract.PrincipalInCharge = Converter.ToEmployeeContract(project.PrincipalInCharge);
                contract.Remarks = project.Remarks;
                contract.Segment = Converter.ToMarketSegmentContract(project.Segment);
                contract.TestingAllowanceAmount = project.TestingAllowanceAmount;
                contract.TotalChangeOrderDays = project.TotalChangeOrderDays;
                contract.TotalChangeOrdersAmount = project.TotalChangeOrdersAmount;
                contract.TotalSquareFeet = project.TotalSquareFeet;
                contract.UtilityAllowanceAmount = project.UtilityAllowanceAmount;
            }
            return contract;
        }

        public static Project ToProject(ProjectContract contract)
        {
            Project project = null;
            if (contract != null)
            {
                project = new Project(contract.Key, contract.Number, contract.Name);
                project.ActualCompletionDate = contract.ActualCompletionDate;
                project.Address = Converter.ToAddress(contract.Address);
                project.AeChangeOrderAmount = contract.AeChangeOrderAmount;
                project.AgencyApplicationNumber = contract.AgencyApplicationNumber;
                project.AgencyFileNumber = contract.AgencyFileNumber;
                foreach (AllowanceContract allowance in contract.Allowances)
                {
                    project.Allowances.Add(Converter.ToAllowance(allowance));
                }
                project.ConstructionAdministrator = Converter.ToEmployee(contract.ConstructionAdministrator);
                foreach (ProjectContactContract contact in contract.Contacts)
                {
                    project.Contacts.Add(Converter.ToProjectContact(contact));
                }
                project.ContingencyAllowanceAmount = contract.ContingencyAllowanceAmount;
                project.ContractDate = contract.ContractDate;
                project.ContractReason = contract.ContractReason;
                foreach (ProjectContractContract projectContract in contract.Contracts)
                {
                    project.Contracts.Add(Converter.ToContract(projectContract));
                }
                project.CurrentCompletionDate = contract.CurrentCompletionDate;
                project.EstimatedCompletionDate = contract.EstimatedCompletionDate;
                project.EstimatedStartDate = contract.EstimatedStartDate;
                project.OriginalConstructionCost = contract.OriginalConstructionCost;
                project.Owner = Converter.ToCompany(contract.Owner);
                project.PercentComplete = contract.PercentComplete;
                project.PrincipalInCharge = Converter.ToEmployee(contract.PrincipalInCharge);
                project.Remarks = contract.Remarks;
                project.Segment = Converter.ToMarketSegment(contract.Segment);
                project.TestingAllowanceAmount = contract.TestingAllowanceAmount;
                project.TotalSquareFeet = contract.TotalSquareFeet;
                project.UtilityAllowanceAmount = contract.UtilityAllowanceAmount;
            }
            return project;
        }

        public static ChangeOrderContract ToChangeOrderContract(ChangeOrder co)
        {
            ChangeOrderContract contract = null;
            if (co != null)
            {
                contract = new ChangeOrderContract();
                contract.AgencyApprovedDate = co.AgencyApprovedDate;
                contract.AmountChanged = co.AmountChanged;
                contract.ArchitectSignatureDate = co.ArchitectSignatureDate;
                contract.ChangeType = Converter.ToPriceChangeTypeContract(co.ChangeType);
                contract.ChangeTypeDirection = Converter.ToChangeDirectionContract(co.PriceChangeDirection);
                contract.Contractor = Converter.ToCompanyContract(co.Contractor);
                contract.ContractorSignatureDate = co.ContractorSignatureDate;
                contract.DateOfSubstantialCompletion = co.DateOfSubstantialCompletion;
                contract.DateToField = co.DateToField;
                contract.Description = co.Description;
                contract.EffectiveDate = co.EffectiveDate;
                contract.Key = co.Key;
                contract.NewConstructionCost = co.NewConstructionCost;
                contract.Number = co.Number;
                contract.OriginalConstructionCost = co.OriginalConstructionCost;
                contract.OwnerSignatureDate = co.OwnerSignatureDate;
                contract.PreviousAuthorizedChangeOrderAmount = co.PreviousAuthorizedAmount;
                contract.PreviousTimeChangedTotal = co.PreviousTimeChangedTotal;
                contract.ProjectKey = co.ProjectKey;
                foreach (RoutingItem item in co.RoutingItems)
                {
                    contract.RoutingItems.Add(Converter.ToRoutingItemContract(item));
                }
                contract.Status = Converter.ToItemStatusContract(co.Status);
                contract.TimeChanged = co.TimeChanged;
                contract.TimeChangeDirection = Converter.ToChangeDirectionContract(co.TimeChangeDirection);
            }
            return contract;
        }

        public static ProposalRequestContract ToProposalRequestContract(ProposalRequest proposalRequest)
        {
            ProposalRequestContract contract = null;
            if (proposalRequest != null)
            {
                contract = new ProposalRequestContract();
                contract.Attachment = proposalRequest.Attachment;
                contract.Cause = proposalRequest.Cause;
                contract.Contractor = Converter.ToCompanyContract(proposalRequest.Contractor);
                foreach (CopyTo item in proposalRequest.CopyToList)
                {
                    contract.CopyToList.Add(Converter.ToCopyListContract(item));
                }
                contract.DeliveryMethod = Converter.ToDeliveryMethodContract(proposalRequest.DeliveryMethod);
                contract.Description = proposalRequest.Description;
                contract.Final = proposalRequest.Final;
                contract.From = Converter.ToEmployeeContract(proposalRequest.From);
                contract.Initiator = proposalRequest.Initiator;
                contract.IssueDate = proposalRequest.IssueDate;
                contract.Key = proposalRequest.Key;
                contract.Number = proposalRequest.Number;
                contract.Origin = proposalRequest.Origin;
                contract.OtherDeliveryMethod = proposalRequest.OtherDeliveryMethod;
                contract.PhaseNumber = proposalRequest.PhaseNumber;
                contract.ProjectKey = proposalRequest.ProjectKey;
                contract.Reason = proposalRequest.Reason;
                contract.Reimbursable = proposalRequest.Reimbursable;
                contract.Remarks = proposalRequest.Remarks;
                contract.To = Converter.ToProjectContactContract(proposalRequest.To);
                contract.TotalPages = proposalRequest.TotalPages;
                contract.TransmittalDate = proposalRequest.TransmittalDate;
                contract.TransmittalRemarks = proposalRequest.TransmittalRemarks;
            }
            return contract;
        }

        public static ProposalRequest ToProposalRequest(ProposalRequestContract contract)
        {
            ProposalRequest proposalRequest = null;
            if (contract != null)
            {
                proposalRequest = new ProposalRequest(contract.Key, contract.ProjectKey, contract.Number);
                proposalRequest.Attachment = contract.Attachment;
                proposalRequest.Cause = contract.Cause;
                proposalRequest.Contractor = Converter.ToCompany(contract.Contractor);
                foreach (CopyToContract item in contract.CopyToList)
                {
                    proposalRequest.CopyToList.Add(Converter.ToCopyTo(item));
                }
                proposalRequest.DeliveryMethod = Converter.ToDeliveryMethod(contract.DeliveryMethod);
                proposalRequest.Description = contract.Description;
                proposalRequest.Final = contract.Final;
                proposalRequest.From = Converter.ToEmployee(contract.From);
                proposalRequest.Initiator = contract.Initiator;
                proposalRequest.IssueDate = contract.IssueDate;
                proposalRequest.Origin = contract.Origin;
                proposalRequest.OtherDeliveryMethod = contract.OtherDeliveryMethod;
                proposalRequest.PhaseNumber = contract.PhaseNumber;
                proposalRequest.Reason = contract.Reason;
                proposalRequest.Reimbursable = contract.Reimbursable;
                proposalRequest.Remarks = contract.Remarks;
                proposalRequest.To = Converter.ToProjectContact(contract.To);
                proposalRequest.TotalPages = contract.TotalPages;
                proposalRequest.TransmittalDate = contract.TransmittalDate;
                proposalRequest.TransmittalRemarks = contract.TransmittalRemarks;
            }
            return proposalRequest;
        }

        private static PriceChangeTypeContract? ToPriceChangeTypeContract(PriceChangeType? priceChangeType)
        {
            PriceChangeTypeContract? contract = null;
            if (priceChangeType.HasValue)
            {
                contract = Enum.Parse(typeof(PriceChangeTypeContract), 
                    priceChangeType.Value.ToString()) as PriceChangeTypeContract?;
            }
            return contract;
        }

        private static PriceChangeType? ToPriceChangeType(PriceChangeTypeContract? contract)
        {
            PriceChangeType? changeType = null;
            if (contract.HasValue)
            {
                changeType = Enum.Parse(typeof(PriceChangeType),
                    contract.Value.ToString()) as PriceChangeType?;
            }
            return changeType;
        }

        private static ChangeDirectionContract ToChangeDirectionContract(ChangeDirection changeDirection)
        {
            return (ChangeDirectionContract)Enum.Parse(typeof(ChangeDirectionContract),
                    changeDirection.ToString());
        }

        private static ChangeDirection ToChangeDirection(ChangeDirectionContract contract)
        {
            return (ChangeDirection)Enum.Parse(typeof(ChangeDirection),
                    contract.ToString());
        }

        private static RoutingItemContract ToRoutingItemContract(RoutingItem item)
        {
            RoutingItemContract contract = null;
            if (item != null)
            {
                contract = new RoutingItemContract();
                contract.DateReturned = item.DateReturned;
                contract.DateSent = item.DateSent;
                contract.Discipline = Converter.ToDisciplineContract(item.Discipline);
                contract.Key = item.Key;
                contract.Recipient = Converter.ToProjectContactContract(item.Recipient);
                contract.RoutingOrder = item.RoutingOrder;
            }
            return contract;
        }

        private static RoutingItem ToRoutingItem(RoutingItemContract contract)
        {
            return (contract!= null ? new RoutingItem(contract.Key, Converter.ToDiscipline(contract.Discipline), 
                       contract.RoutingOrder, Converter.ToProjectContact(contract.Recipient), 
                       contract.DateSent, contract.DateReturned) : null);
        }

        private static DisciplineContract ToDisciplineContract(Discipline discipline)
        {
            DisciplineContract contract = null;
            if (discipline != null)
            {
                contract = new DisciplineContract();
                contract.Description = discipline.Description;
                contract.Key = discipline.Key;
                contract.Name = discipline.Name;
            }
            return contract;
        }

        private static Discipline ToDiscipline(DisciplineContract contract)
        {
            return (contract!= null ? new Discipline(contract.Key, contract.Name, contract.Description) : null);
        }

        private static ItemStatusContract ToItemStatusContract(ItemStatus status)
        {
            ItemStatusContract contract = null;
            if (status != null)
            {
                contract = new ItemStatusContract();
                contract.Id = status.Id;
                contract.Status = status.Status;
            }
            return contract;
        }

        private static ItemStatus ToItemStatus(ItemStatusContract contract)
        {
            return (contract != null ? new ItemStatus(contract.Id, contract.Status) : null);
        }

        public static ChangeOrder ToChangeOrder(ChangeOrderContract contract)
        {
            ChangeOrder co = null;
            if (contract != null)
            {
                co = new ChangeOrder(contract.Key, contract.ProjectKey, contract.Number);
                co.AgencyApprovedDate = contract.AgencyApprovedDate;
                co.AmountChanged = contract.AmountChanged;
                co.ArchitectSignatureDate = contract.ArchitectSignatureDate;
                co.ChangeType = Converter.ToPriceChangeType(contract.ChangeType);
                co.PriceChangeDirection = Converter.ToChangeDirection(contract.ChangeTypeDirection);
                co.Contractor = Converter.ToCompany(contract.Contractor);
                co.ContractorSignatureDate = contract.ContractorSignatureDate;
                co.DateToField = contract.DateToField;
                co.Description = contract.Description;
                co.EffectiveDate = contract.EffectiveDate;
                co.OwnerSignatureDate = contract.OwnerSignatureDate;
                foreach (RoutingItemContract item in contract.RoutingItems)
                {
                    co.RoutingItems.Add(Converter.ToRoutingItem(item));
                }
                co.Status = Converter.ToItemStatus(contract.Status);
                co.TimeChanged = contract.TimeChanged;
                co.TimeChangeDirection = Converter.ToChangeDirection(contract.TimeChangeDirection);
            }
            return co;
        }

        public static ConstructionChangeDirectiveContract ToConstructionChangeDirectiveContract(ConstructionChangeDirective ccd)
        {
            ConstructionChangeDirectiveContract contract = null;
            if (ccd != null)
            {
                contract = new ConstructionChangeDirectiveContract();
                contract.AmountChanged = ccd.AmountChanged;
                contract.ArchitectSignatureDate = ccd.ArchitectSignatureDate;
                contract.Attachment = ccd.Attachment;
                contract.Cause = ccd.Cause;
                contract.ChangeType = Converter.ToPriceChangeTypeContract(ccd.ChangeType);
                contract.Contractor = Converter.ToCompanyContract(ccd.Contractor);
                contract.ContractorSignatureDate = ccd.ContractorSignatureDate;
                foreach (CopyTo copy in ccd.CopyToList)
                {
                    contract.CopyToList.Add(Converter.ToCopyListContract(copy));
                }
                contract.DeliveryMethod = Converter.ToDeliveryMethodContract(ccd.DeliveryMethod);
                contract.Description = ccd.Description;
                contract.Final = ccd.Final;
                contract.From = Converter.ToEmployeeContract(ccd.From);
                contract.Initiator = ccd.Initiator;
                contract.IssueDate = ccd.IssueDate;
                contract.Key = ccd.Key;
                contract.Number = ccd.Number;
                contract.Origin = ccd.Origin;
                contract.OtherDeliveryMethod = ccd.OtherDeliveryMethod;
                contract.OwnerSignatureDate = ccd.OwnerSignatureDate;
                contract.PhaseNumber = ccd.PhaseNumber;
                contract.PriceChangeDirection = Converter.ToChangeDirectionContract(ccd.PriceChangeDirection);
                contract.ProjectKey = ccd.ProjectKey;
                contract.Reason = ccd.Reason;
                contract.Reimbursable = ccd.Reimbursable;
                contract.Remarks = ccd.Remarks;
                contract.TimeChanged = ccd.TimeChanged;
                contract.TimeChangeDirection = Converter.ToChangeDirectionContract(ccd.TimeChangeDirection);
                contract.To = Converter.ToProjectContactContract(ccd.To);
                contract.TotalPages = ccd.TotalPages;
                contract.TransmittalDate = ccd.TransmittalDate;
                contract.TransmittalRemarks = ccd.TransmittalRemarks;
            }
            return contract;
        }

        public static ConstructionChangeDirective ToConstructionChangeDirective(ConstructionChangeDirectiveContract contract)
        {
            ConstructionChangeDirective ccd = null;
            if (contract != null)
            {
                ccd = new ConstructionChangeDirective(contract.Key, contract.ProjectKey, contract.Number);
                ccd.AmountChanged = contract.AmountChanged;
                ccd.ArchitectSignatureDate = contract.ArchitectSignatureDate;
                ccd.Attachment = contract.Attachment;
                ccd.Cause = contract.Cause;
                ccd.ChangeType = Converter.ToPriceChangeType(contract.ChangeType);
                ccd.Contractor = Converter.ToCompany(contract.Contractor);
                ccd.ContractorSignatureDate = contract.ContractorSignatureDate;
                foreach (CopyToContract item in contract.CopyToList)
                {
                    ccd.CopyToList.Add(Converter.ToCopyTo(item));
                }
                ccd.DeliveryMethod = Converter.ToDeliveryMethod(contract.DeliveryMethod);
                ccd.Description = contract.Description;
                ccd.Final = contract.Final;
                ccd.From = Converter.ToEmployee(contract.From);
                ccd.Initiator = contract.Initiator;
                ccd.IssueDate = contract.IssueDate;
                ccd.Origin = contract.Origin;
                ccd.OtherDeliveryMethod = contract.OtherDeliveryMethod;
                ccd.OwnerSignatureDate = contract.OwnerSignatureDate;
                ccd.PhaseNumber = contract.PhaseNumber;
                ccd.PriceChangeDirection = Converter.ToChangeDirection(contract.PriceChangeDirection);
                ccd.Reason = contract.Reason;
                ccd.Reimbursable = contract.Reimbursable;
                ccd.Remarks = contract.Remarks;
                ccd.TimeChanged = contract.TimeChanged;
                ccd.TimeChangeDirection = Converter.ToChangeDirection(contract.TimeChangeDirection);
                ccd.To = Converter.ToProjectContact(contract.To);
                ccd.TotalPages = contract.TotalPages;
                ccd.TransmittalDate = contract.TransmittalDate;
                ccd.TransmittalRemarks = contract.TransmittalRemarks;
            }
            return ccd;
        }

        public static SubmittalContract ToSubmittalContract(Submittal submittal)
        {
            SubmittalContract contract = null;
            if (submittal != null)
            {
                contract = new SubmittalContract();
                contract.Action = Converter.ToActionStatusContract(submittal.Action);
                contract.ContractNumber = submittal.ContractNumber;
                foreach (CopyTo copyTo in submittal.CopyToList)
                {
                    contract.CopyToList.Add(Converter.ToCopyListContract(copyTo));
                }
                contract.DateReceived = submittal.DateReceived;
                contract.DateToField = submittal.DateToField;
                contract.DeliveryMethod = Converter.ToDeliveryMethodContract(submittal.DeliveryMethod);
                contract.Final = submittal.Final;
                contract.From = Converter.ToEmployeeContract(submittal.From);
                contract.Key = submittal.Key;
                contract.OtherDeliveryMethod = submittal.OtherDeliveryMethod;
                contract.OtherRemainderLocation = submittal.OtherRemainderLocation;
                contract.PhaseNumber = submittal.PhaseNumber;
                contract.ProjectKey = submittal.ProjectKey;
                contract.Reimbursable = submittal.Reimbursable;
                contract.RemainderLocation = Converter.ToRemainderLocationContract(submittal.RemainderLocation);
                contract.RemainderUnderSubmittalNumber = submittal.RemainderUnderSubmittalNumber;
                contract.Remarks = submittal.Remarks;
                foreach (RoutingItem item in submittal.RoutingItems)
                {
                    contract.RoutingItems.Add(Converter.ToRoutingItemContract(item));
                }
                contract.SpecSection = Converter.ToSpecificationSectionContract(submittal.SpecSection);
                contract.SpecSectionPrimaryIndex = submittal.SpecSectionPrimaryIndex;
                contract.SpecSectionSecondaryIndex = submittal.SpecSectionSecondaryIndex;
                contract.Status = Converter.ToItemStatusContract(submittal.Status);
                contract.To = Converter.ToProjectContactContract(submittal.To);
                contract.TotalPages = submittal.TotalPages;
                foreach (TrackingItem item in submittal.TrackingItems)
                {
                    contract.TrackingItems.Add(Converter.ToTrackingItemContract(item));
                }
                contract.TransmittalDate = submittal.TransmittalDate;
                contract.TransmittalRemarks = submittal.TransmittalRemarks;
            }
            return contract;
        }

        private static SubmittalRemainderLocationContract ToRemainderLocationContract(SubmittalRemainderLocation submittalRemainderLocation)
        {
            return (SubmittalRemainderLocationContract)Enum.Parse(typeof(SubmittalRemainderLocationContract),
                    submittalRemainderLocation.ToString());
        }

        public static Submittal ToSubmittal(SubmittalContract contract)
        {
            Submittal submittal = null;
            if (contract != null)
            {
                submittal = new Submittal(contract.Key, 
                    Converter.ToSpecificationSection(contract.SpecSection), contract.ProjectKey);
                submittal.Action = Converter.ToActionStatus(contract.Action);
                submittal.ContractNumber = contract.ContractNumber;
                foreach (CopyToContract copyTo in contract.CopyToList)
                {
                    submittal.CopyToList.Add(Converter.ToCopyTo(copyTo));
                }
                submittal.DateReceived = contract.DateReceived;
                submittal.DateToField = contract.DateToField;
                submittal.DeliveryMethod = Converter.ToDeliveryMethod(contract.DeliveryMethod);
                submittal.Final = contract.Final;
                submittal.From = Converter.ToEmployee(contract.From);
                submittal.OtherDeliveryMethod = contract.OtherDeliveryMethod;
                submittal.OtherRemainderLocation = contract.OtherRemainderLocation;
                submittal.PhaseNumber = contract.PhaseNumber;
                submittal.Reimbursable = contract.Reimbursable;
                submittal.RemainderLocation = Converter.ToRemainderLocation(contract.RemainderLocation);
                submittal.RemainderUnderSubmittalNumber = contract.RemainderUnderSubmittalNumber;
                submittal.Remarks = contract.Remarks;
                foreach (RoutingItemContract item in contract.RoutingItems)
                {
                    submittal.RoutingItems.Add(Converter.ToRoutingItem(item));
                }
                submittal.SpecSection = Converter.ToSpecificationSection(contract.SpecSection);
                submittal.SpecSectionPrimaryIndex = contract.SpecSectionPrimaryIndex;
                submittal.SpecSectionSecondaryIndex = contract.SpecSectionSecondaryIndex;
                submittal.Status = Converter.ToItemStatus(contract.Status);
                submittal.To = Converter.ToProjectContact(contract.To);
                submittal.TotalPages = contract.TotalPages;
                foreach (TrackingItemContract item in contract.TrackingItems)
                {
                    submittal.TrackingItems.Add(Converter.ToTrackingItem(item));
                }
                submittal.TransmittalDate = contract.TransmittalDate;
                submittal.TransmittalRemarks = contract.TransmittalRemarks;
            }
            return submittal;
        }

        private static SubmittalRemainderLocation ToRemainderLocation(SubmittalRemainderLocationContract contract)
        {
            return (SubmittalRemainderLocation)Enum.Parse(typeof(SubmittalRemainderLocation),
                    contract.ToString());
        }

        private static ActionStatus ToActionStatus(ActionStatusContract actionStatusContract)
        {
            return (ActionStatus)Enum.Parse(typeof(ActionStatus),
                    actionStatusContract.ToString());
        }

        private static TrackingItemContract ToTrackingItemContract(TrackingItem item)
        {
            TrackingItemContract contract = null;
            if (item != null)
            {
                contract = new TrackingItemContract();
                contract.DeferredApproval = item.DeferredApproval;
                contract.Description = item.Description;
                contract.Status = Converter.ToActionStatusContract(item.Status);
                contract.SubstitutionNumber = item.SubstitutionNumber;
                contract.SpecSection = Converter.ToSpecificationSectionContract(item.SpecSection);
                contract.TotalItemsReceived = item.TotalItemsReceived;
                contract.TotalItemsSent = item.TotalItemsSent;
            }
            return contract;
        }

        private static TrackingItem ToTrackingItem(TrackingItemContract contract)
        {
            TrackingItem item = null;
            if (contract != null)
            {
                item = new TrackingItem(Converter.ToSpecificationSection(contract.SpecSection));
                item.DeferredApproval = contract.DeferredApproval;
                item.Description = contract.Description;
                item.Status = Converter.ToActionStatus(contract.Status);
                item.SubstitutionNumber = contract.SubstitutionNumber;
                item.TotalItemsReceived = contract.TotalItemsReceived;
                item.TotalItemsSent = contract.TotalItemsSent;
            }
            return item;
        }

        private static ActionStatusContract ToActionStatusContract(ActionStatus actionStatus)
        {
            return (ActionStatusContract)Enum.Parse(typeof(ActionStatusContract),
                    actionStatus.ToString());
        }

        public static RequestForInformationContract ToRequestForInformationContract(RequestForInformation rfi)
        {
            RequestForInformationContract contract = null;
            if (rfi != null)
            {
                contract = new RequestForInformationContract();
                contract.Cause = rfi.Cause;
                contract.Change = rfi.Change;
                contract.Contractor = Converter.ToCompanyContract(rfi.Contractor);
                contract.ContractorProposedSolution = rfi.ContractorProposedSolution;
                foreach (CopyTo copyTo in rfi.CopyToList)
                {
                    contract.CopyToList.Add(Converter.ToCopyListContract(copyTo));
                }
                contract.DateReceived = rfi.DateReceived;
                contract.DateRequestedBy = rfi.DateRequestedBy;
                contract.DateToField = rfi.DateToField;
                contract.DeliveryMethod = Converter.ToDeliveryMethodContract(rfi.DeliveryMethod);
                contract.Description = rfi.Description;
                contract.Final = rfi.Final;
                contract.From = Converter.ToProjectContactContract(rfi.From);
                contract.Key = rfi.Key;
                contract.LongAnswer = rfi.LongAnswer;
                contract.Number = rfi.Number;
                contract.Origin = rfi.Origin;
                contract.OtherDeliveryMethod = rfi.OtherDeliveryMethod;
                contract.PhaseNumber = rfi.PhaseNumber;
                contract.ProjectKey = rfi.ProjectKey;
                contract.Question = rfi.Question;
                contract.Reimbursable = rfi.Reimbursable;
                contract.Remarks = rfi.Remarks;
                foreach (RoutingItem item in rfi.RoutingItems)
                {
                    contract.RoutingItems.Add(Converter.ToRoutingItemContract(item));
                }
                contract.ShortAnswer = rfi.ShortAnswer;
                contract.SpecSection = Converter.ToSpecificationSectionContract(rfi.SpecSection);
                contract.Status = Converter.ToItemStatusContract(rfi.Status);
                contract.TotalPages = rfi.TotalPages;
                contract.TransmittalDate = rfi.TransmittalDate;
            }
            return contract;
        }

        public static SpecificationSectionContract ToSpecificationSectionContract(SpecificationSection specSection)
        {
            SpecificationSectionContract contract = null;
            if (specSection != null)
            {
                contract = new SpecificationSectionContract();
                contract.Description = specSection.Description;
                contract.Key = specSection.Key;
                contract.Number = specSection.Number;
                contract.Title = specSection.Title;
            }
            return contract;
        }

        public static SpecificationSection ToSpecificationSection(SpecificationSectionContract contract)
        {
            SpecificationSection specSection = null;
            if (contract != null)
            {
                specSection = new SpecificationSection(contract.Key, contract.Number, 
                    contract.Title, contract.Description);
            }
            return specSection;
        }

        public static RequestForInformation ToRequestForInformation(RequestForInformationContract contract)
        {
            RequestForInformation rfi = null;
            if (contract != null)
            {
                rfi = new RequestForInformation(contract.Key, contract.ProjectKey, contract.Number);
                rfi.Cause = contract.Cause;
                rfi.Change = contract.Change;
                rfi.Contractor = Converter.ToCompany(contract.Contractor);
                rfi.ContractorProposedSolution = contract.ContractorProposedSolution;
                foreach (CopyToContract copyTo in contract.CopyToList)
                {
                    rfi.CopyToList.Add(Converter.ToCopyTo(copyTo));
                }
                rfi.DateReceived = contract.DateReceived;
                rfi.DateRequestedBy = contract.DateRequestedBy;
                rfi.DateToField = contract.DateToField;
                rfi.DeliveryMethod = Converter.ToDeliveryMethod(contract.DeliveryMethod);
                rfi.Description = contract.Description;
                rfi.Final = contract.Final;
                rfi.From = Converter.ToProjectContact(contract.From);
                rfi.LongAnswer = contract.LongAnswer;
                rfi.Origin = contract.Origin;
                rfi.OtherDeliveryMethod = contract.OtherDeliveryMethod;
                rfi.PhaseNumber = contract.PhaseNumber;
                rfi.Question = contract.Question;
                rfi.Reimbursable = contract.Reimbursable;
                rfi.Remarks = contract.Remarks;
                foreach (RoutingItemContract item in contract.RoutingItems)
                {
                    rfi.RoutingItems.Add(Converter.ToRoutingItem(item));
                }
                rfi.ShortAnswer = contract.ShortAnswer;
                rfi.SpecSection = Converter.ToSpecificationSection(contract.SpecSection);
                rfi.Status = Converter.ToItemStatus(contract.Status);
                rfi.TotalPages = contract.TotalPages;
                rfi.TransmittalDate = contract.TransmittalDate;
                rfi.TransmittalRemarks = contract.Remarks;
            }
            return rfi;
        }

        private static DeliveryContract ToDeliveryMethodContract(Delivery delivery)
        {
            return (DeliveryContract)Enum.Parse(typeof(DeliveryContract),
                    delivery.ToString());
        }

        private static Delivery ToDeliveryMethod(DeliveryContract contract)
        {
            return (Delivery)Enum.Parse(typeof(Delivery),
                    contract.ToString());
        }

        private static CopyToContract ToCopyListContract(CopyTo copy)
        {
            CopyToContract contract = null;
            if (copy != null)
            {
                contract = new CopyToContract();
                contract.Contact = Converter.ToProjectContactContract(copy.Contact);
                contract.Notes = copy.Notes;
            }
            return contract;
        }

        private static CopyTo ToCopyTo(CopyToContract contract)
        {
            CopyTo copy = null;
            if (contract != null)
            {
                copy = new CopyTo(Converter.ToProjectContact(contract.Contact), 
                    contract.Notes);
            }
            return copy;
        }

        public static ClientMembershipUserContract ToClientMembershipUserContract(ClientMembershipUser user)
        {
            ClientMembershipUserContract contract = null;
            if (user != null)
            {
                contract = new ClientMembershipUserContract();
                contract.CreationDate = user.CreationDate;
                contract.Email = user.Email;
                contract.FailedPasswordAnswerAttemptCount = user.FailedPasswordAnswerAttemptCount;
                contract.FailedPasswordAnswerAttemptWindowStart = user.FailedPasswordAnswerAttemptWindowStart;
                contract.FailedPasswordAttemptCount = user.FailedPasswordAttemptCount;
                contract.FailedPasswordAttemptWindowStart = user.FailedPasswordAttemptWindowStart;
                contract.IsLockedOut = user.IsLockedOut;
                contract.Key = user.Key;
                contract.LastActivityDate = user.LastActivityDate;
                contract.LastLockoutDate = user.LastLockoutDate;
                contract.LastLoginDate = user.LastLoginDate;
                contract.LastPasswordChangedDate = user.LastPasswordChangedDate;
                contract.PasswordFormat = Converter.ToPasswordFormatContract(user.PasswordFormat);
                contract.PasswordQuestion = user.PasswordQuestion;
                contract.PasswordSalt = user.PasswordSalt;
                foreach (Role role in user.Roles)
                {
                    contract.Roles.Add(Converter.ToRoleContract(role));
                }
                contract.UserKey = user.UserKey;
                contract.UserName = user.UserName;
            }
            return contract;
        }

        public static ClientMembershipUser ToClientMembershipUser(ClientMembershipUserContract contract)
        {
            ClientMembershipUser user = null;
            if (contract != null)
            {
                user = new ClientMembershipUser(new User(contract.UserKey,
                    contract.UserName, contract.Email, contract.PasswordQuestion,
                    contract.IsLockedOut, contract.CreationDate, contract.LastLoginDate,
                    contract.LastActivityDate, contract.LastPasswordChangedDate,
                    contract.LastLockoutDate), contract.PasswordSalt, 
                    Converter.ToPasswordFormat(contract.PasswordFormat),
                    contract.FailedPasswordAttemptCount,
                    contract.FailedPasswordAttemptWindowStart,
                    contract.FailedPasswordAnswerAttemptCount,
                    contract.FailedPasswordAnswerAttemptWindowStart);
                foreach (RoleContract role in contract.Roles)
                {
                    user.Roles.Add(Converter.ToRole(role));
                }
            }
            return user;
        }

        public static RoleContract ToRoleContract(Role role)
        {
            RoleContract contract = null;
            if (role != null)
            {
                contract = new RoleContract();
                contract.Name = role.Name;
            }
            return contract;
        }

        public static Role ToRole(RoleContract contract)
        {
            Role role = null;
            if (contract != null)
            {
                role = new Role(contract.Name);
            }
            return role;
        }

        private static PasswordFormatContract ToPasswordFormatContract(PasswordFormat passwordFormat)
        {
            return (PasswordFormatContract)Enum.Parse(typeof(PasswordFormatContract),
                    passwordFormat.ToString());
        }

        private static PasswordFormat ToPasswordFormat(PasswordFormatContract contract)
        {
            return (PasswordFormat)Enum.Parse(typeof(PasswordFormat),
                    contract.ToString());
        }
    }
}