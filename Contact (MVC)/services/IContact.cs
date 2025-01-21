using Contact__MVC_.Models;

namespace Contact__MVC_.services
{
    public interface IContact
    {
        string constring();
        bool AddContact(ContactData contact);
        List<ContactData> GetContacts();
        bool DeleteContact(ContactData contact);
        List<ContactData> GetContactById(ContactData contact);
        bool UpdateContact (ContactData contact);
        bool UploadImage(ContactData contact);
        List<ContactData> Imagefetched(ContactData contact);

    }
}
