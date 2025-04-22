using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Primitives;
using System.Text;

public class SqliteStorage : IStorage
{
    string connectionString = "Data Source=contacts.db";

    public List<Contact> Contacts
    {
        get
        {
            List<Contact> contacts = new();
            
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM contacts";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                contacts.Add(new Contact(reader.GetInt32(0))
                {
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Phone = reader.GetString(3),
                    Email = reader.GetString(4)
                });
            }

            return contacts;
        }
    }

    public bool Add(ContactDto contactDto, out Contact contact)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        contact = new(1);

        var command = connection.CreateCommand();
        var sqlQuerry = new StringBuilder()
            .Append("INSERT INTO contacts(first_name, last_name, phone_number, email)\n")
            .Append($"VALUES ('{contactDto.FirstName}', '{contactDto.LastName}', '{contactDto.Phone}', '{contactDto.Email}')")
            .ToString();
        command.CommandText = sqlQuerry;

        return command.ExecuteNonQuery() == 1;
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
