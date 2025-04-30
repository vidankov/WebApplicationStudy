using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Primitives;
using System.Text;

public class SqliteStorage : IStorage
{
    private readonly string? connectionString;

    public SqliteStorage(string? connectionString)
    {
        this.connectionString = connectionString;
    }

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

        var command = connection.CreateCommand();

        var sqlQuerry = new StringBuilder()
            .Append("INSERT INTO contacts(first_name, last_name, phone_number, email)\n")
            .Append($"VALUES (@firstName, @lastName, @phone, @email)")
            .ToString();

        command.CommandText = sqlQuerry;

        command.Parameters.AddWithValue("@firstName", contactDto.FirstName);
        command.Parameters.AddWithValue("@lastName", contactDto.LastName);
        command.Parameters.AddWithValue("@phone", contactDto.Phone);
        command.Parameters.AddWithValue("@email", contactDto.Email);

        bool isSuccessful = command.ExecuteNonQuery() == 1;

        command.CommandText = "SELECT MAX(id) FROM contacts;";

        long latestId = (long)command.ExecuteScalar();
        contact = GetContactById((int)latestId);

        return isSuccessful;
    }

    public Contact? GetContactById(int id)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = $"SELECT * FROM contacts WHERE id = {id};";

        using var reader = command.ExecuteReader();

        if (!reader.Read()) 
        {
            return null;
        }

        var contact = new Contact(reader.GetInt32(0))
        {
            FirstName = reader.GetString(1),
            LastName = reader.GetString(2),
            Phone = reader.GetString(3),
            Email = reader.GetString(4)
        };

        return contact;
    }

    public (List<Contact>, int TotalCount) GetContacts(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public bool Remove(int id)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        string sqlQuerry = $"DELETE FROM contacts WHERE id = {id}";

        command.CommandText = sqlQuerry;

        return command.ExecuteNonQuery() == 1;
    }

    public bool Update(int id, ContactDto contactDto, out Contact? contact)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        var sqlQuerry = new StringBuilder()
            .Append("UPDATE contacts SET ")
            .Append($"first_name = @firstName, ")
            .Append($"last_name = @lastName, ")
            .Append($"phone_number = @phone, ")
            .Append($"email = @email ")
            .Append("WHERE id = @id;")
            .ToString();

        command.CommandText = sqlQuerry;

        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@firstName", contactDto.FirstName);
        command.Parameters.AddWithValue("@lastName", contactDto.LastName);
        command.Parameters.AddWithValue("@phone", contactDto.Phone);
        command.Parameters.AddWithValue("@email", contactDto.Email);

        /*
            contact.Email = ValidateNewValue(contactDto.Email) ? contactDto.Email : contact.Email;
            contact.Phone = ValidateNewValue(contactDto.Phone) ? contactDto.Phone : contact.Phone;
            contact.FirstName = ValidateNewValue(contactDto.FirstName) ? contactDto.FirstName : contact.FirstName;
            contact.LastName = ValidateNewValue(contactDto.LastName) ? contactDto.LastName : contact.LastName;
         */

        bool isSuccessful = command.ExecuteNonQuery() == 1;

        command.CommandText = "SELECT MAX(id) FROM contacts;";

        long latestId = (long)command.ExecuteScalar();
        contact = GetContactById((int)latestId);

        return isSuccessful;
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
