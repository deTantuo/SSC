using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonApi.Core;
using PersonApi.Core.Domain;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PersonApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/Person")]
    [Authorize(Policy = "ValidAccessToken")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        
        public PersonController(IUnitOfWork unitOfWork, ILogger<PersonController> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddPerson(Person person)
        {
            _logger.LogInformation("-----The user made the call is: {User.Identity.Name}");
            if (person == null) return BadRequest();

            try
            {
                await _unitOfWork.Person.Add(person);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            
            return Ok();
        }

        //GET api/v1/[controller]/persons
        
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("-----The user made the call is: {User.Identity.Name}");
            if (id <= 0) return BadRequest();

            var data = await  _unitOfWork.Person.Get(id);

            if (data == null) return NotFound();
           
            return Ok(data);
        }

        
        [HttpGet]
        [Route("personList")]
        [ProducesResponseType(typeof(IEnumerable<PersonList>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PersonList()
        {
            _logger.LogInformation("-----The user made the call is: {User.Identity.Name}");
            var data = await _unitOfWork.Person.GetPersonList();

            return Ok(data);
        }

        //GET api/v1/[controller]/persons
        
        [HttpGet]
        [Route("persons")]
        [ProducesResponseType(typeof(IEnumerable<Person>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPersons()
        {
            _logger.LogInformation("-----The user made the call is: {User.Identity.Name}");
            var data = await _unitOfWork.Person.GetAll();

            return Ok(data);
        }

        
        [HttpGet]
        [Route("state")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPersonByState([FromBody]string state)
        {
            _logger.LogInformation("-----The user made the call is: {User.Identity.Name}");
            if (state == null) return BadRequest();

            var data = await  _unitOfWork.Person.Find(p => p.state.ToLower() == state.ToLower());

            if (data == null) return NotFound();

            return Ok(data);
        }

        
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult DeletePerson(Person person)
        {
            _logger.LogInformation("-----The user made the call is: {User.Identity.Name}");
            if (person == null) return NotFound();

            try
            {
                _unitOfWork.Person.Remove(person);
                _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
           
            return NoContent();

        }

        [HttpDelete]
        [Route("softdelete")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Person person)
        {
            _logger.LogInformation("-----The user made the call is: {User.Identity.Name}");
            if (person == null) return NotFound();
            try
            {
                var softDeletePerson = await _unitOfWork.Person.Get(person.id);
                softDeletePerson.isDeleted = false;
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        
        [HttpPut]
        [Route("update")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdatePerson([FromBody] PersonUpdateDTO personDTO)
        {
            _logger.LogInformation("-----The user made the call is: {User.Identity.Name}");
            if (personDTO == null)  return NotFound();
                  
            try
            {
                await _unitOfWork.Person.Update(personDTO);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }
    }
}
