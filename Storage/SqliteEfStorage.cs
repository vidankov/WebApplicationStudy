
public class SqliteEfStorage : IStorage
{
    private readonly SqliteDbContext _context;

    public List<Contact> Contacts => _context.Contacts.ToList();

    public SqliteEfStorage(SqliteDbContext context)
    {
        _context = context;
    }

    public bool Add(ContactDto contactDto, out Contact contact)
    {
        contact = new Contact(null);
        contact.FirstName = contactDto.FirstName;
        contact.LastName = contactDto.LastName;
        contact.Email = contactDto.Email;
        contact.Phone = contactDto.Phone;

        _context.Contacts.Add(contact);
        _context.SaveChanges();

        return true;
    }

    public Contact? GetContactById(int id)
    {
        return _context.Contacts.Find(id);
    }

    public bool Remove(int id)
    {
        var contact = _context.Contacts.Find(id);
        if (contact == null)
        {
            return false;
        }
        _context.Contacts.Remove(contact);
        _context.SaveChanges();
        return true;
    }

    public bool Update(int id, ContactDto contactDto, out Contact? contact)
    {
        contact = _context.Contacts.Find(id);
        if (contact == null)
        {
            return false;
        }
        contact.FirstName = contactDto.FirstName;
        contact.LastName = contactDto.LastName;
        contact.Email = contactDto.Email;
        contact.Phone = contactDto.Phone;
        _context.SaveChanges();
        return true;
    }

    public (List<Contact>, int TotalCount) GetContacts(int pageNumber, int pageSize)
    {
        int total = _context.Contacts.Count();
        List<Contact> contacts = _context.Contacts
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        return (contacts, total);
    }
}
