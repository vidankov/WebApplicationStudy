using Bogus;

public class ContactStorage
{
    private List<Contact> _contacts = [];

    public List<Contact> Contacts { get { return _contacts; } }

    public ContactStorage()
    {
        _contacts = new Faker<Contact>("ru")
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("7-###-###-####"))
            .RuleFor(c => c.Email, (f, t) => f.Internet.Email(t.FirstName, t.LastName))
            .Generate(5);
    }

    public void Add(ContactDto contactDto)
    {
        Contact contact = new();
        contact.FirstName = contactDto.FirstName;
        contact.LastName = contactDto.LastName;
        contact.Phone = contactDto.Phone;
        contact.Email = contactDto.Email;
        _contacts.Add(contact);
    }

    public void Remove(int id)
    {
        _contacts.Remove(_contacts.First(c => c.Id == id));
    }

    public void Update(int id, ContactDto contactDto)
    {
        var contact = _contacts.First(c => c.Id == id);
        contact.Email = ValidateNewValue(contactDto.Email) ? contactDto.Email : contact.Email;
        contact.Phone = ValidateNewValue(contactDto.Phone) ? contactDto.Phone : contact.Phone;
        contact.FirstName = ValidateNewValue(contactDto.FirstName) ? contactDto.FirstName : contact.FirstName;
        contact.LastName = ValidateNewValue(contactDto.LastName) ? contactDto.LastName : contact.LastName;
    }

    protected bool ValidateNewValue(string? value)
    {
        if (string.IsNullOrEmpty(value) || value == "string")
        {
            return false;
        }
        return true;
    }
}
