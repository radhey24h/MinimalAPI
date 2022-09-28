using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Employee.Dal.Repository.AsyncCommonRepository;
using Employee.Dal.Repository.CommonRepository;
using Employee.ControllerWebApi.DTO;
using Emp=Employee.Models.Entities;

namespace Employee.ControllerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IAsyncCommonRepository<Emp.Employee> _asyncEmployeeRepository;
        private ICommonRepository<Emp.Employee> _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IAsyncCommonRepository<Emp.Employee> asyncEmployeeRepository,ICommonRepository<Emp.Employee> repository, IMapper mapper)
        {
            _asyncEmployeeRepository = asyncEmployeeRepository;
            _employeeRepository = repository;
            _mapper = mapper;
        }
        /*[HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<Employee>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            var employees= _employeeRepository.GetAll();
            if (!employees.Any())
            {
                return NotFound();
            }
            return Ok(employees);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDetails(int id)
        {
            var employee= _employeeRepository.GetDetails(id);
            if (employee==null)
            {
                return NotFound();

            }
            return Ok(employee);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Employee,Hr")]
        public ActionResult<IEnumerable<EmployeeDto>> Get()
        {
            var employees = _employeeRepository.GetAll();
            if (employees.Count <= 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }*/
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Employee,Hr")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
        {
            var employees = await _asyncEmployeeRepository.GetAll();
            if (employees.Count <= 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Employee,Hr")]
        public ActionResult<EmployeeDto> GetDetails(int id)
        {
            var employee = _employeeRepository.GetDetails(id);
            return employee == null ? NotFound() : Ok(_mapper.Map<EmployeeDto>(employee));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Employee,Hr")]
        public ActionResult Create(NewEmployeeDto employee)
        {
            var employeeModel = _mapper.Map<Emp.Employee>(employee);
            _employeeRepository.Insert(employeeModel);
            var result = _employeeRepository.SaveChanges();
            if (result > 0)
            {
                //actionName - The name of the action to use for generating the URL
                //routeValues - The route data to use for generating the URL
                //value - The content value to format in the entity body
                var employeeDetails = _mapper.Map<EmployeeDto>(employeeModel);
                return CreatedAtAction("GetDetails", new { id = employeeDetails.EmployeeId }, employeeDetails);
            }
            return BadRequest();
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Employee,Hr")]
        public ActionResult Update(UpdateEmployeeDto employee)
        {
            var employeeModel = _mapper.Map<Emp.Employee>(employee);
            _employeeRepository.Update(employeeModel);
            var result = _employeeRepository.SaveChanges();
            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Hr")]
        public ActionResult<Emp.Employee> Delete(int id)
        {
            var employee = _employeeRepository.GetDetails(id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                _employeeRepository.Delete(employee);
                _employeeRepository.SaveChanges();
                return NoContent();
            }
        }
    }
}
