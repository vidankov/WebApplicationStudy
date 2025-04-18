using Microsoft.Data.Sqlite;

public class SqliteStorage : IStorage
{
    public List<Contact> Contacts
    {
        get
        {
            List<Contact> contacts = new();

            string connectionString = "Data Source=contacts.db";
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
