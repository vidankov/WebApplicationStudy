using Bogus;

public class InMemoryStorage : IStorage
{
    private List<Contact> _contacts = [];

    public List<Contact> Contacts => _contacts;

    public int MaxSize { get; }

    public InMemoryStorage(int maxSize)
    {
        MaxSize = maxSize;

        _contacts = new Faker<Contact>("ru")
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("7-###-###-####"))
            .RuleFor(c => c.Email, (f, t) => f.Internet.Email(t.FirstName, t.LastName))
            .Generate(5);
    }

    public Contact? GetContactById(int id) => _contacts.FirstOrDefault(c => c.Id == id);

    public bool Add(ContactDto contactDto, out Contact contact)
    {
        contact = new(1);

        if (Contacts.Count == MaxSize)
        {
            return false;
        }

        contact.FirstName = contactDto.FirstName;
        contact.LastName = contactDto.LastName;
        contact.Phone = contactDto.Phone;
        contact.Email = contactDto.Email;

        _contacts.Add(contact);

        return true;
    }

    public bool Remove(int id)
    {
        var contact = GetContactById(id);
        if (contact is not null)
        {
            _contacts.Remove(contact);
            return true;
        }
        return false;
    }

    public bool Update(int id, ContactDto contactDto, out Contact? contact)
    {
        contact = GetContactById(id);
        if (contact is not null)
        {
            contact.Email = ValidateNewValue(contactDto.Email) ? contactDto.Email : contact.Email;
            contact.Phone = ValidateNewValue(contactDto.Phone) ? contactDto.Phone : contact.Phone;
            contact.FirstName = ValidateNewValue(contactDto.FirstName) ? contactDto.FirstName : contact.FirstName;
            contact.LastName = ValidateNewValue(contactDto.LastName) ? contactDto.LastName : contact.LastName;
            return true;
        }
        return false;
    }

    protected bool ValidateNewValue(string? value)
    {
        if (string.IsNullOrEmpty(value) || value == "string")
        {
            return false;
        }
        return true;
    }

    public (List<Contact>, int TotalCount) GetContacts(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }
}
