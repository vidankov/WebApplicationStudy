using Microsoft.AspNetCore.Mvc;

public class ContactManagementController : BaseController
{
    private readonly ContactStorage _storage;

    public ContactManagementController(ContactStorage storage)
    {
        _storage = storage;
    }

    [HttpPost("contacts")]
    public void Create([FromBody] ContactDto contactDto)
    {
        _storage.Add(contactDto);
    }

    [HttpDelete("contacts/{id}")]
    public void Delete(int id) 
    {
        _storage.Remove(id);
    }

    [HttpPut("contacts/{id}")]
    public void Update(int id, ContactDto contactDto)
    {
        _storage.Update(id, contactDto);
    }

    [HttpGet("contacts")]
    public List<Contact> GetAllContacts()
    {
        return _storage.Contacts;
    }
}
