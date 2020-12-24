using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// <c>GenresController</c> is a class.
    /// Contains all http methods for working with genres.
    /// </summary>
    /// <remarks>
    /// This class can get, create, delete, edit genres.
    /// </remarks>

    // api/genres
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        /// <summary>
        /// This method returns all genres
        /// </summary>
        /// <response code="200">Returns all genres</response>

        //GET api/genres
        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreService.GetAllAsync();
            return Ok(genres);
        }

        /// <summary>
        /// This method returns genre that has an inputted Id property
        /// </summary>
        /// <response code="200">Returns genre that has an inputted Id property</response>
        /// <response code="404">Returns message that nothing was found, if message wasn't returned than id inputted incorrectly</response>

        //GET api/genres/{id}
        [HttpGet("{id:int}", Name = "GetGenreById")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            try
            {
                var genre = await _genreService.GetByIdAsync(id);
                return Ok(genre);
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method returns genre that was created and path to it
        /// </summary>
        /// <response code="201">Returns genre that was created and path to it</response>
        /// <response code="400">Returns message why model is invalid</response>
        /// <response code="404">Returns message if something had gone wrong</response>

        //POST api/genres 
        [HttpPost]
        [ProducesResponseType(typeof(GenreDTO), 201)]
        public async Task<IActionResult> CreateGenre(GenreDTO genreDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (genreDto.Id != 0)
                    return BadRequest("The Id should be empty");

                var createdGenre = await _genreService.AddAsync(genreDto);

                //Fetch the brand from data source
                return CreatedAtRoute("GetGenreById", new {id = createdGenre.Id}, createdGenre);
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method changes genre
        /// </summary>
        /// <response code="204">Returns nothing, genre was successfully changed</response>
        /// <response code="400">Returns message why model is invalid</response>
        /// <response code="404">Returns message that genre was not found, if message wasn't returned than id inputted incorrectly</response>

        //PUT api/genres
        [HttpPut]
        [ProducesResponseType(204)]
        public IActionResult UpdateGenre(GenreDTO genreDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _genreService.Update(genreDto);
                return NoContent();
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method deletes genre
        /// </summary>
        /// <response code="204">Returns nothing, genre was successfully deleted</response>
        /// <response code="404">Returns message that genre was not found</response>

        //DELETE api/genre/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        public IActionResult DeleteGenre(int id)
        {
            try
            {
                _genreService.Remove(id);
                return NoContent();
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}