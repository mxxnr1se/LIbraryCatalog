using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// <c>AuthorsController</c> is a class.
    /// Contains all http methods for working with authors.
    /// </summary>
    /// <remarks>
    /// This class can get, create, delete, edit authors.
    /// </remarks>

    // api/suppliers
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// This method returns all authors
        /// </summary>
        /// <response code="200">Returns all authors</response>

        //GET api/authors
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }

        /// <summary>
        /// This method returns author that has an inputted Id property
        /// </summary>
        /// <response code="200">Returns author that has an inputted Id property</response>
        /// <response code="404">Returns message that nothing was found, if message wasn't returned than id inputted incorrectly</response>

        //GET api/authors/{id}
        [HttpGet("{id:int}", Name = "GetauthorById")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            try
            {
                var author = await _authorService.GetByIdAsync(id);
                return Ok(author);
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method returns author that was created and path to it
        /// </summary>
        /// <response code="201">Returns author that was created and path to it</response>
        /// <response code="400">Returns message why model is invalid</response>
        /// <response code="404">Returns message if something had gone wrong</response>

        //POST api/suppliers 
        [HttpPost]
        [ProducesResponseType(typeof(AuthorDTO), 201)]
        public async Task<IActionResult> CreateAuthor(AuthorDTO authorDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (authorDto.Id != 0)
                    return BadRequest("The Id should be empty");

                var createdAuthor = await _authorService.AddAsync(authorDto);

                //Fetch the author from data source
                return CreatedAtRoute("GetAuthorById", new {id = createdAuthor.Id}, createdAuthor);
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method changes author
        /// </summary>
        /// <response code="204">Returns nothing, author was successfully changed</response>
        /// <response code="400">Returns message why model is invalid</response>
        /// <response code="404">Returns message that author was not found, if message wasn't returned than id inputted incorrectly</response>

        //PUT api/authors
        [HttpPut]
        [ProducesResponseType(204)]
        public IActionResult UpdateAuthor(AuthorDTO authorDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _authorService.Update(authorDto);
                return NoContent();
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method deletes author
        /// </summary>
        /// <response code="204">Returns nothing, author was successfully deleted</response>
        /// <response code="404">Returns message that author was not found</response>

        //DELETE api/authors/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        public IActionResult DeleteAuthor(int id)
        {
            try
            {
                _authorService.Remove(id);
                return NoContent();
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}