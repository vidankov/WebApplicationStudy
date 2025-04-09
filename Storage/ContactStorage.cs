using Bogus;

public class ContactStorage
{
    public List<Contact> Contacts = [];

    public ContactStorage()
    {
        Contacts = [];

        int ids = 1;
        var faker = new Faker<Contact>("ru")
            .RuleFor(c => c.Id, f => ids++)
            .RuleFor(c => c.Name, f => f.Name.FullName())
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("7-###-###-####"))
            .RuleFor(c => c.Email, f => f.Internet.Email());

        for (int i = 0; i < 5; i++)
        {
            Contacts.Add(faker.Generate());
        }
    }
}
