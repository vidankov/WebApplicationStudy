using Microsoft.AspNetCore.Mvc;

public class ContactManagementController : BaseController
{
    private readonly ContactStorage _storage;

    public ContactManagementController(ContactStorage storage)
    {
        _storage = storage;
    }

    [HttpPost("contacts")]
    public void Create([FromBody] Contact contact)
    {
        _storage.Contacts.Add(contact);
    }

    [HttpDelete("contacts/{id}")]
    public void Delete(int id) 
    {
        _storage.Contacts.Remove(_storage.Contacts.First(c => c.Id == id));
    }

    [HttpPut("contacts/{id}")]
    public void Update(int id, ContactDto contactDto)
    {
        var contact = _storage.Contacts.First(c => c.Id == id);
        contact.Email = ValidateNewValue(contactDto.Email) ? contactDto.Email : contact.Email;
        contact.Phone = ValidateNewValue(contactDto.Phone) ? contactDto.Phone : contact.Phone;
        contact.FirstName = ValidateNewValue(contactDto.FirstName) ? contactDto.FirstName : contact.FirstName;
        contact.LastName = ValidateNewValue(contactDto.LastName) ? contactDto.LastName : contact.LastName;
    }

    [HttpGet("contacts")]
    public List<Contact> GetContacts()
    {
        return _storage.Contacts;
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
