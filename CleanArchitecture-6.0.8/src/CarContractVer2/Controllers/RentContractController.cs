using CleanArchitecture.Application.Repository;
using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.RentContract;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class RentContractController : ControllerBase
    {
        private readonly IRentContractRepository _rentContractRepository;


        public RentContractController(IRentContractRepository rentContractRepository)
        {
            _rentContractRepository = rentContractRepository;
        }

        [HttpGet]
        [Route(RentContractEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RentContract>))]
        public IActionResult GetRentContractById(int id)
        {
            if (!_rentContractRepository.RentContractExit(id))
                return NotFound();
            var rentContract = _rentContractRepository.GetRentContractById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rentContract);
        }

        [HttpGet]
        [Route(RentContractEndpoints.GetByContractGroupId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RentContract>))]
        public IActionResult GetRentContractByContractGroupId(int contractGroupId)
        {
            var rentContract = _rentContractRepository.GetRentContractByContractGroupId(contractGroupId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rentContract);
        }


        [HttpPost]
        [Route(RentContractEndpoints.Create)]
        public IActionResult CreaterentContract([FromBody] RentContractCreateModel request)
        {
            _rentContractRepository.CreateRentContract(request);
            return Ok();
        }

        [HttpPut]
        [Route(RentContractEndpoints.Update)]
        public IActionResult Update(int id, [FromBody] RentContractUpdateModel request)
        {
            if (request == null || id != request.Id)
                return BadRequest();

            // Check if the car with the specified id exists
            if (!_rentContractRepository.RentContractExit(id))
                return NotFound();

            // Update the car and its related data
            _rentContractRepository.UpdateRentContract(id, request);

            return Ok();
        }

        [HttpPut]
        [Route(RentContractEndpoints.UpdateContractStatus)]
        public IActionResult UpdateCarStatus([FromRoute] int id, [FromBody] RentContractUpdateStatusModel request)
        {
            if (request == null || id != request.Id)
                return BadRequest();
            if (!_rentContractRepository.RentContractExit(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_rentContractRepository.UpdateRentContractStatus(id, request))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}


  
