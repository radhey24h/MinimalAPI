using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Employee.Dal.Repository.CommonRepository;
using Employee.Models.Entities;

namespace Employee.ControllerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private ICommonRepository<Event> _eventRepository;
        public EventController(ICommonRepository<Event> repository)
        {
            _eventRepository = repository;

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Event>> Get()
        {
            var employees = _eventRepository.GetAll();
            if (!employees.Any())
            {
                return NotFound();
            }
            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Event> GetDetails(int id)
        {
            var evnt = _eventRepository.GetDetails(id);
            if (evnt == null)
            {
                return NotFound();
            }
            return Ok(evnt);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create(Event evnt)
        {
            _eventRepository.Insert(evnt);
            var result= _eventRepository.SaveChanges();
            if (result > 0)
            {
                return CreatedAtAction("GetDetails", new { id = evnt.EventId }, evnt);
            }
            return BadRequest();
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(Event evnt)
        {
            _eventRepository.Update(evnt);
            var result = _eventRepository.SaveChanges();
            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest();
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            var evnt = _eventRepository.GetDetails(id);
            if (evnt == null)
            {
                return NotFound();
            }
            else
            {
                _eventRepository.Delete(evnt);
                _eventRepository.SaveChanges();
                return NoContent();
            }
        }
    }
}
