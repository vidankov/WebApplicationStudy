
public class SqliteStorage : IStorage
{
    public List<Contact> Contacts => throw new NotImplementedException();

    public bool Add(ContactDto contactDto, out Contact contact)
    {
        throw new NotImplementedException();
    }

    public Contact? GetContactById(int id)
    {
        throw new NotImplementedException();
    }

    public bool Remove(int id)
    {
        throw new NotImplementedException();
    }

    public bool Update(int id, ContactDto contactDto, out Contact? contact)
    {
        throw new NotImplementedException();
    }
}
