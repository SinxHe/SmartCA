using System;
using SmartCA.Infrastructure;
using SmartCA.Infrastructure.RepositoryFramework;

namespace SmartCA.Model.Contacts
{
    public static class ContactService
    {
        private static IContactRepository repository;
        private static IUnitOfWork unitOfWork;

        static ContactService()
        {
            ContactService.unitOfWork = new UnitOfWork();
            ContactService.repository = 
                RepositoryFactory.GetRepository<IContactRepository,
                Contact>(ContactService.unitOfWork);
        }

        public static void SaveContact(Contact contact)
        {
            ContactService.repository[contact.Key] = contact;
            ContactService.unitOfWork.Commit();
        }

        public static Contact GetContact(object contactKey)
        {
            return ContactService.repository.FindBy(contactKey);
        }
    }
}
