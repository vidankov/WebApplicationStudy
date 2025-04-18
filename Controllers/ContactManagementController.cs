using Microsoft.AspNetCore.Mvc;

public class ContactManagementController : BaseController
{
    private readonly IStorage _storage;

    public ContactManagementController(IStorage storage)
    {
        _storage = storage;
    }

    [HttpPost("contacts")]
    public IActionResult Create([FromBody] ContactDto contactDto)
    {
        if (_storage.Add(contactDto, out Contact contact))
        {
            return Created($"api/ContactManagement/contacts/{contact.Id}", contact);
        }
        return Conflict("Превышено максимальное количество контактов");
    }

    [HttpDelete("contacts/{id}")]
    public IActionResult Delete(int id) 
    {
        if (_storage.Remove(id))
        {
            return NoContent();
        }
        return BadRequest("Контакт с указанным ID не найден");
    }

    [HttpPut("contacts/{id}")]
    public IActionResult Update(int id, ContactDto contactDto)
    {
        if (_storage.Update(id, contactDto, out Contact? contact))
        {
            return Ok(contact);
        }
        return Conflict("Контакт с указанным ID не найден");
    }

    [HttpGet("contacts")]
    public ActionResult<List<Contact>> GetAllContacts()
    {
        return Ok(_storage.Contacts);
    }

    [HttpGet("contacts/{idString}")]
    public ActionResult<Contact> GetContact(string idString)
    {
        if (!int.TryParse(idString, out int id))
        {
            return BadRequest("ID должен быть целым числом");
        }
        
        var contact = _storage.GetContactById(id);
        
        if (contact is null)
        {
            return NotFound("Контакт с указанным ID не найден");
        }

        return Ok(contact);
    }
}
