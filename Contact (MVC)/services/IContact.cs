using Contact__MVC_.Models;

namespace Contact__MVC_.services
{
    public interface IContact
    {
        string constring();
        bool AddContact(ContactData contact);

        List<ContactData> GetContacts();

    }
}
