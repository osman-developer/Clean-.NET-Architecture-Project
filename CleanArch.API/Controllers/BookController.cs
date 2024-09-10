using ClearnArch.Domain.DTOs.Book;
using ClearnArch.Domain.Entities;
using ClearnArch.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("save")]
        public ActionResult<bool> SaveBook([FromBody] AddBookDTO book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            return Ok(_bookService.Save(book));
        }

        [HttpGet("books")]
        public async Task<ActionResult<List<GetBookDTO>>> GetBooks()
        {
            return Ok(await _bookService.GetAll());
        }

        [HttpPost("book")]
        public async Task<ActionResult<Book>> GetBookById([FromBody] int bookId)
        {
            return Ok(await _bookService.Get(bookId));
        }

        [HttpPost("delete")]
        public ActionResult<bool> DeleteBook([FromBody] int bookId)
        {
            return Ok(_bookService.Delete(bookId));
        }
    }
}
