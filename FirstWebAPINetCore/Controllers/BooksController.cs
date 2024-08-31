using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstWebAPINetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly ILogger<BooksController> _logger;
        private readonly IConfiguration _configuration;

        public BooksController(ILogger<BooksController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync()
        {
            return await Task.FromResult(new string[] { "value1", "value2" });
        }

        // GET: api/<BooksController>
        [HttpGet("numbers")]
        public async Task<IEnumerable<int>> GetBooksAsync()
        {
            return await Task.FromResult(new int[] { 1, 2 });
        }


        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BooksController>
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return Created("/books/10", new { Id = 10, Message = "Libro creado correctamente" });
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
