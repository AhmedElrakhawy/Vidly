using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VidlyAPI.Models;
using VidlyAPI.Models.DTO;
using VidlyAPI.Repository.IRepository;

namespace VidlyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/Customer")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _Mapper;
        public CustomersController(ICustomersRepository customersRepository, IMapper mapper)
        {
            _customersRepository = customersRepository;
            _Mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CustomerDto>))]
        public IActionResult GetCustomers()
        {
            var Customers = _customersRepository.GetAllCustomers();
            var CustomerDtoList = new List<CustomerDto>();
            foreach (var customer in Customers)
            {
                CustomerDtoList.Add(_Mapper.Map<CustomerDto>(customer));
            }

            return Ok(CustomerDtoList);
        }


        [HttpGet("{Id:int}", Name = "GetCustomer")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(CustomerDto))]
        public IActionResult GetCustomer(int Id)
        {
            var Customer = _customersRepository.GetCustomer(Id);
            if (Customer == null)
                return NotFound();
            var CustomerDto = _Mapper.Map<CustomerDto>(Customer);
            return Ok(CustomerDto);
        }


        [HttpPost]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        [ProducesResponseType(201, Type = typeof(Customer))]
        [ProducesDefaultResponseType]
        public IActionResult CreateCustomer([FromBody] CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Customer = _Mapper.Map<Customer>(customerDto);
            if (!_customersRepository.CreateCustomer(Customer))
            {
                ModelState.AddModelError("", $"Something went wrong while creating , {Customer.Name} is invalid.");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCustomer", new { Id = Customer.Id }, Customer);
            //return Created(nameof(GetCustomer), new { Id = Customer.Id });
        }


        [HttpPatch("{Id:int}")]
        [ProducesResponseType(500)]
        [ProducesResponseType(409)]
        [ProducesResponseType(204)]
        public IActionResult UpdateCustomer(int Id, [FromBody] CustomerDto customerDto)
        {

            if (customerDto == null || customerDto.Id != Id)
                return BadRequest(ModelState);

            var Customer = _Mapper.Map<Customer>(customerDto);
            if (!_customersRepository.UpdateCustomer(Customer))
            {
                ModelState.AddModelError("", $"Something went wrong while Updating , {Customer.Name} is invalid.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{Id:int}")]
        [ProducesResponseType(500)]
        [ProducesResponseType(409)]
        [ProducesResponseType(204)]
        public IActionResult DeleteCustomer(int Id)
        {
            var CustomerInDb = _customersRepository.CustomerExcistbyId(Id);
            if (CustomerInDb == false)
                return NotFound(ModelState);

            var Customer = _customersRepository.GetCustomer(Id);
            if (!_customersRepository.DeleteCustomer(Customer))
            {
                ModelState.AddModelError("", $"Something went wrong while Deleting , {Customer.Name} is invalid.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
