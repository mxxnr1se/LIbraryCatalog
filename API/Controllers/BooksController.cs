using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// <c>BooksController</c> is a class.
    /// Contains all http methods for working with books.
    /// </summary>
    /// <remarks>
    /// This class can get, create, delete, edit books.
    /// </remarks>

    // api/books
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// This method returns all books
        /// </summary>
        /// <response code="200">Returns all books</response>

        //GET api/books
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        /// <summary>
        /// This method returns book that has an inputted Id property
        /// </summary>
        /// <response code="200">Returns book that has an inputted Id property</response>
        /// <response code="404">Returns message that nothing was found, if message wasn't returned than id inputted incorrectly</response>

        //GET api/books/{id}
        [HttpGet("{id:int}", Name = "GetBookById")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await _bookService.GetByIdAsync(id);
                return Ok(book);
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method returns book that has an inputted GenreId property
        /// </summary>
        /// <response code="200">Returns book that has an inputted GenreId property</response>
        /// <response code="404">Returns message that nothing was found, if message wasn't returned than id inputted incorrectly</response>

        //Get api/books/bygenre/{id}
        [HttpGet("bygenre/{id:int}", Name = "GetBooksByGenreId")]
        public IActionResult GetBooksByGenreId(int id)
        {
            try
            {
                var books = _bookService.GetBooksByGenreId(id);
                return Ok(books);
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method returns book that has an inputted AuthorId property
        /// </summary>
        /// <response code="200">Returns book that has an inputted AuthorId property</response>
        /// <response code="404">Returns message that nothing was found, if message wasn't returned than id inputted incorrectly</response>

        //Get api/books/byauthor/{id}
        [HttpGet("byauthor/{id:int}", Name = "GetBooksByAuthorId")]
        public IActionResult GetBooksByAuthorId(int id)
        {
            try
            {
                var books = _bookService.GetBooksByAuthorId(id);
                return Ok(books);
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method returns book that was created and path to it
        /// </summary>
        /// <response code="201">Returns book that was created and path to it</response>
        /// <response code="400">Returns message why model is invalid</response>
        /// <response code="404">Returns message if something had gone wrong</response>

        //POST api/books 
        [HttpPost]
        [ProducesResponseType(typeof(BookDTO), 201)]
        public async Task<IActionResult> CreateBook(BookDTO bookDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (bookDto.Id != 0)
                    return BadRequest("The Id should be empty");

                var createdBook = await _bookService.AddAsync(bookDto);

                //Fetch the book from data source
                return CreatedAtRoute("GetBookById", new {id = createdBook.Id}, createdBook);
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method changes book
        /// </summary>
        /// <response code="204">Returns nothing, book was successfully changed</response>
        /// <response code="400">Returns message why model is invalid</response>
        /// <response code="404">Returns message that book was not found, if message wasn't returned than id inputted incorrectly</response>

        //PUT api/books
        [HttpPut]
        [ProducesResponseType(204)]
        public IActionResult UpdateBook(BookDTO bookDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _bookService.Update(bookDto);
                return NoContent();
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method deletes book
        /// </summary>
        /// <response code="204">Returns nothing, book was successfully deleted</response>
        /// <response code="404">Returns message that book was not found</response>

        //DELETE api/book/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _bookService.Remove(id);
                return NoContent();
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}