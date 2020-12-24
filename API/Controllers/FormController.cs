using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// <c>FormsController</c> is a class.
    /// Contains all http methods for working with forms.
    /// </summary>
    /// <remarks>
    /// This class can get, create, delete, edit forms.
    /// </remarks>

    // api/forms
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly IFormService _formService;

        public FormsController(IFormService formService)
        {
            _formService = formService;
        }

        /// <summary>
        /// This method returns all forms
        /// </summary>
        /// <response code="200">Returns all forms</response>

        //GET api/forms
        [HttpGet]
        public async Task<IActionResult> GetAllForms()
        {
            var forms = await _formService.GetAllAsync();
            return Ok(forms);
        }

        /// <summary>
        /// This method returns form that has an inputted Id property
        /// </summary>
        /// <response code="200">Returns form that has an inputted Id property</response>
        /// <response code="404">Returns message that nothing was found, if message wasn't returned than id inputted incorrectly</response>

        //GET api/forms/{id}
        [HttpGet("{id:int}", Name = "GetFormById")]
        public async Task<IActionResult> GetFormById(int id)
        {
            try
            {
                var form = await _formService.GetByIdAsync(id);
                return Ok(form);
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }
        
        /// <summary>
        /// This method returns form that has an inputted readerId property
        /// </summary>
        /// <response code="200">Returns form that has an inputted readerId property</response>
        /// <response code="404">Returns message that nothing was found, if message wasn't returned than id inputted incorrectly</response>

        //GET api/forms/reader/{readerid}
        [HttpGet("reader/{id:int}", Name = "GetReaderBooks")]
        public IActionResult GetReaderBooks(int id)
        {
            try
            {
                var form = _formService.GetReaderBooks(id);
                return Ok(form);
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }
        
        /// <summary>
        /// This method returns form that has an inputted bookId property
        /// </summary>
        /// <response code="200">Returns form that has an inputted bookId property</response>
        /// <response code="404">Returns message that nothing was found, if message wasn't returned than id inputted incorrectly</response>

        //GET api/forms/book/{bookid}
        [HttpGet("book/{id:int}", Name = "GetBookReaders")]
        public IActionResult GetBookReaders(int id)
        {
            try
            {
                var form = _formService.GetBookReaders(id);
                return Ok(form);
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }
        

        /// <summary>
        /// This method returns form that was created and path to it
        /// </summary>
        /// <response code="201">Returns form that was created and path to it</response>
        /// <response code="400">Returns message why model is invalid</response>
        /// <response code="404">Returns message if something had gone wrong</response>

        //POST api/forms 
        [HttpPost]
        [ProducesResponseType(typeof(FormDTO), 201)]
        public async Task<IActionResult> CreateForm(FormDTO formDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (formDto.Id != 0)
                    return BadRequest("The Id should be empty");

                var createdForm = await _formService.AddAsync(formDto);

                //Fetch the form from data source
                return CreatedAtRoute("GetFormById", new {id = createdForm.Id}, createdForm);
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method changes form
        /// </summary>
        /// <response code="204">Returns nothing, form was successfully changed</response>
        /// <response code="400">Returns message why model is invalid</response>
        /// <response code="404">Returns message that form was not found, if message wasn't returned than id inputted incorrectly</response>

        //PUT api/forms
        [HttpPut]
        [ProducesResponseType(204)]
        public IActionResult UpdateForm(FormDTO formDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _formService.Update(formDto);
                return NoContent();
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// This method deletes form
        /// </summary>
        /// <response code="204">Returns nothing, form was successfully deleted</response>
        /// <response code="404">Returns message that form was not found</response>

        //DELETE api/forms/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        public IActionResult DeleteForm(int id)
        {
            try
            {
                _formService.Remove(id);
                return NoContent();
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }
        
        /// <summary>
        /// This method deletes form that has an inputted readerId,bookId properties
        /// </summary>
        /// <response code="204">Returns nothing, form was successfully deleted</response>
        /// <response code="404">Returns message that form was not found</response>

        //DELETE api/forms/{readerid}/{bookid}
        [HttpDelete("remove/{readerId:int}/{bookId:int}")]
        [ProducesResponseType(204)]
        public IActionResult RemoveBook(int readerId, int bookId)
        {
            try
            {
                _formService.RemoveBook(readerId, bookId);
                return NoContent();
            }
            catch (ResultException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}