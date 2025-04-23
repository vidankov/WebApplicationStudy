using Bogus;
using Microsoft.Data.Sqlite;

public class FakerInitializer : IInitializer
{
    private readonly string _connectionString;

    public FakerInitializer(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Initialize()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();

        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS contacts(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                first_name TEXT NOT NULL,
                last_name TEXT NOT NULL,
                phone_number TEXT,
                email TEXT NOT NULL
            );
        ";
        command.ExecuteNonQuery();

        command.CommandText = "SELECT COUNT(*) FROM contacts";

        long count = (long)command.ExecuteScalar();

        if (count == 0)
        {
            var contacts = new Faker<Contact>("ru")
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("7-###-###-####"))
            .RuleFor(c => c.Email, (f, t) => f.Internet.Email(t.FirstName, t.LastName))
            .Generate(20);

            foreach (var contact in contacts)
            {
                command.CommandText = @"
                    INSERT INTO
                        contacts(first_name, last_name, phone_number, email)
                    VALUES
                        (@firstName, @lastName, @phoneNumber, @email);
                ";

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@firstName", contact.FirstName);
                command.Parameters.AddWithValue("@lastName", contact.LastName);
                command.Parameters.AddWithValue("@phoneNumber", contact.Phone);
                command.Parameters.AddWithValue("@email", contact.Email);
                command.ExecuteNonQuery();
            }
        }
    }
}
