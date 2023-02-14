using Microsoft.AspNetCore.Mvc;
using ToDo_List.Models;
using ToDo_List.Services;

namespace ToDo_List.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : Controller
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IToDoService _toDoService;

        public ToDoController(
            ILogger<WeatherForecastController> logger,
            IToDoService toDoService
            )
        {
            _logger = logger;
            _toDoService = toDoService;
        }

       // [HttpPost(Name = "ToDo")]
        //public async Task<IActionResult> Add([FromBody] ToDoItem item)
        //{
        //    await _toDoService.CreateAsync(item);
        //    return Ok();
        //}

        [HttpPost(Name = "ToDo")]
        public async Task<IActionResult> Post([FromBody] ToDoItem item)
        {
            await _toDoService.CreateAsync(item);

            return CreatedAtAction(nameof(Get), new { id = item.ItemId }, item);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<ToDoItem>> Get(string id)
        {
            var book = await _toDoService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, ToDoItem item)
        {
            var existingItem = await _toDoService.GetAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            item.ItemId = existingItem.ItemId;

            await _toDoService.UpdateAsync(id, item);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await _toDoService.GetAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            await _toDoService.RemoveAsync(id);

            return NoContent();
        }
    }
}
