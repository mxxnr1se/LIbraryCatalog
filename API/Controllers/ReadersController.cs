using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// <c>ReadersController</c> is a class.
    /// Contains all http methods for working with readers.
    /// </summary>
    /// <remarks>
    /// This class can get, create, delete, edit readers.
    /// </remarks>

    // api/readers
    [Route("api/[controller]")]
    [ApiController]
    public class ReadersController : ControllerBase
    {
        private readonly IReaderService _readerService;

        public ReadersController(IReaderService readerService)
        {
            _readerService = readerService;
        }

        /// <summary>
        /// This method returns all readers
        /// </summary>
        /// <response code="200">Returns all categories</response>

        //GET api/readers
        [HttpGet]
        public async Task<IActionResult> GetAllReaders()
        {
            var readers = await _readerService.GetAllAsync();
            return Ok(readers);
        }

        /// <summary>
        /// This method returns reader that has an inputted Id property
        /// </summary>
        /// <response code="200">Returns reader that has an inputted Id property</response>
        /// <response code="404">Returns message that nothing was found, if message wasn't returned than id inputted incorrectly</response>

        //GET api/readers/{id}
        [HttpGet("{id:int}", Name = "GetReaderById")]
        public async Task<IActionResult> GetReaderById(int id)
        {
            try
            {
                var reader = await _readerService.GetByIdAsync(id);
                return Ok(reader);
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method returns reader that was created and path to it
        /// </summary>
        /// <response code="201">Returns reader that was created and path to it</response>
        /// <response code="400">Returns message why model is invalid</response>
        /// <response code="404">Returns message if something had gone wrong</response>

        //POST api/readers 
        [HttpPost]
        [ProducesResponseType(typeof(ReaderDTO), 201)]
        public async Task<IActionResult> CreateReader(ReaderDTO readerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (readerDto.Id != 0)
                    return BadRequest("The Id should be empty");

                var createdReader = await _readerService.AddAsync(readerDto);

                //Fetch the reader from data source
                return CreatedAtRoute("GetReaderById", new {id = createdReader.Id}, createdReader);
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method changes category
        /// </summary>
        /// <response code="204">Returns nothing, reader was successfully changed</response>
        /// <response code="400">Returns message why model is invalid</response>
        /// <response code="404">Returns message that category was not found, if message wasn't returned than id inputted incorrectly</response>

        //PUT api/readers
        [HttpPut]
        [ProducesResponseType(204)]
        public IActionResult UpdateReader(ReaderDTO readerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _readerService.Update(readerDto);
                return NoContent();
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method deletes reader
        /// </summary>
        /// <response code="204">Returns nothing, category was successfully deleted</response>
        /// <response code="404">Returns message that category was not found</response>

        //DELETE api/readers/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        public IActionResult DeleteReader(int id)
        {
            try
            {
                _readerService.Remove(id);
                return NoContent();
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}