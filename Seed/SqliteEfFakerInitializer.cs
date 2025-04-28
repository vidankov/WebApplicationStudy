using Bogus;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

public class SqliteEfFakerInitializer : IInitializer
{
    private readonly SqliteDbContext _context;

    public SqliteEfFakerInitializer(SqliteDbContext context)
    {
        _context = context;
    }

    public void Initialize()
    {
        _context.Database.Migrate();

        if (!_context.Contacts.Any())
        {
            var contacts = new Faker<Contact>("ru")
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("7-###-###-####"))
            .RuleFor(c => c.Email, (f, t) => f.Internet.Email(t.FirstName, t.LastName))
            .Generate(20);

            _context.Contacts.AddRange(contacts);
            _context.SaveChanges();
        }

    }
}
