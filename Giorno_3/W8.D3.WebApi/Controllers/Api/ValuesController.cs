using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace W8.D3.WebApi.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly List<int> numbers = [];

        [HttpGet]
        public ActionResult<IEnumerable<int>> ReadAllNumbers() {
            return Ok(numbers);
        }

        [HttpGet("{position}")]
        public ActionResult<int> ReadByPosition([FromRoute] int position) {
            try {
                return Ok(numbers[position]);
            }
            catch {
                return BadRequest("Indice non interno all'array");
            }
        }

        [HttpGet("slice/{from}/to/{to}")]
        public ActionResult<IEnumerable<int>> Slice([FromRoute] int from, [FromRoute] int to) {
            try {
                return Ok(numbers[from..to]);
            }
            catch {
                return BadRequest(new {
                    ErrorMessage = $"Indici non validi [{from}..{to}]",
                    From = from, To = to, Size = numbers.Count
                });
            }
        }

        [HttpPost]
        public IActionResult AddNumber([FromBody] int number) {
            try {
                if (number > 10000) throw new Exception();
                numbers.Add(number);
                return CreatedAtAction(nameof(ReadByPosition), new { position = numbers.Count - 1 }, number);
            }
            catch {
                return StatusCode(416, "Non accettabile");
            }
        }

        [HttpPut]
        public IActionResult UpdateCollection([FromBody] IEnumerable<int> newCollection) {
            try {
                numbers.Clear();
                numbers.AddRange(newCollection);
                return NoContent();
            }
            catch {
                return BadRequest();
            }
        }

        [HttpPut("{position}")]
        public IActionResult UpdateNumber([FromBody] int newNumber, [FromRoute] int position) {
            numbers[position] = newNumber;
            return Ok();
        }

        [HttpPut("slice/{from}/to/{to}")]
        public IActionResult UpdateSlice([FromBody] IEnumerable<int> items,
            [FromRoute] int from, [FromRoute] int to) {
            numbers.RemoveRange(from, to);
            numbers.InsertRange(from, items);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteCollection() {
            try {
                numbers.Clear();
                return Accepted();
            }
            catch {
                return BadRequest();
            }
        }
        [HttpDelete("{position}")]
        public IActionResult Delete([FromRoute] int position) {
            numbers.RemoveAt(position);
            return Ok();
        }
        [HttpDelete("slice/{from}/to/{to}")]
        public IActionResult DeleteSlice([FromRoute] int from, [FromRoute] int to) {
            numbers.RemoveRange(from, to);
            return Ok();
        }
    }
}
