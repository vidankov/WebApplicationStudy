﻿using Microsoft.AspNetCore.Mvc;

public class ContactManagementController : BaseController
{
    private readonly ContactStorage _storage;

    public ContactManagementController(ContactStorage storage)
    {
        _storage = storage;
    }

    [HttpPost("contacts")]
    public IActionResult Create([FromBody] ContactDto contactDto)
    {
        if (_storage.Add(contactDto, out Contact contact))
        {
            return Created();
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
        return BadRequest("Контакт с указанным ID не найдено");
    }

    [HttpPut("contacts/{id}")]
    public IActionResult Update(int id, ContactDto contactDto)
    {
        if (_storage.Update(id, contactDto))
        {
            return Ok();
        }
        return Conflict("Контакт с указанным ID не найдено");
    }

    [HttpGet("contacts")]
    public ActionResult<List<Contact>> GetAllContacts()
    {
        return Ok(_storage.Contacts);
    }
}
