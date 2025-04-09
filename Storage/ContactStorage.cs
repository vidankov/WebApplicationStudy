using Bogus;

public class ContactStorage
{
    public List<Contact> Contacts = [];

    public ContactStorage()
    {
        int ids = 1;

        Contacts = new Faker<Contact>("ru")
            .RuleFor(c => c.Id, f => ids++)
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("7-###-###-####"))
            .RuleFor(c => c.Email, (f, t) => f.Internet.Email(t.FirstName, t.LastName))
            .Generate(5);
    }
}
