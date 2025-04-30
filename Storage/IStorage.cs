public interface IStorage
{
    List<Contact> Contacts { get; }

    (List<Contact>, int TotalCount) GetContacts(int pageNumber, int pageSize);

    Contact? GetContactById(int id);
    
    bool Add(ContactDto contactDto, out Contact contact);
    
    bool Remove(int id);

    bool Update(int id, ContactDto contactDto, out Contact? contact);
}
