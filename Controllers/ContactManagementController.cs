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
}
