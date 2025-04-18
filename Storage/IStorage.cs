public interface IStorage
{
    List<Contact> Contacts { get; }

    Contact? GetContactById(int id);
    
    bool Add(ContactDto contactDto, out Contact contact);
    
    bool Remove(int id);

    bool Update(int id, ContactDto contactDto, out Contact? contact);
}
