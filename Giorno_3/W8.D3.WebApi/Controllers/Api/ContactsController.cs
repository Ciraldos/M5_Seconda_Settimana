using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using W8.D3.WebApi.Controllers.Api.Models;

namespace W8.D3.WebApi.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private static readonly HashSet<Contact> _contacts = [
            new (){ Name = "Paperino", Phone = "12345677", Id = 1 },
            new (){ Name = "Paperone", Phone = "31235345124", Id = 2 },
            new (){ Name = "Topolino", Phone = "123451425", Id = 3 }
            ];

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetAll() {
            return Ok(_contacts);
        }

        [HttpGet("{id}")]
        public ActionResult<Contact> Get([FromRoute] int id) {
            try {
                return Ok(_contacts.Single(c => c.Id == id));
            }
            catch {
                return NotFound($"Impossibile recuperare il contatto con id = {id}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Contact contact) {
            try {
                var id = _contacts.Select(c => c.Id).DefaultIfEmpty(0).Max();
                contact.Id = id + 1;
                _contacts.Add(contact);
                return CreatedAtAction(nameof(Get), new { id = contact.Id }, contact);
            }
            catch {
                return StatusCode(416, "Non accettabile");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Contact contact, [FromRoute] int id) {
            try {
                var old = _contacts.Single(c => c.Id == id);
                old.Name = contact.Name;
                old.Phone = contact.Phone;
                return NoContent();
            }
            catch {
                return NotFound($"Impossibile recuperare il contatto con id = {id}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id) {
            try {
                var old = _contacts.Single(c => c.Id == id);
                _contacts.Remove(old);
                return Accepted();
            }
            catch {
                return NotFound($"Impossibile recuperare il contatto con id = {id}");
            }
        }
    }
}
