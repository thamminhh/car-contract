using CleanArchitecture.Application.Repository;
using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ReceiveContract;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class ReceiveContractController : ControllerBase
    {
        private readonly IReceiveContractRepository _receiveContractRepository;


        public ReceiveContractController(IReceiveContractRepository receiveContractRepository)
        {
            _receiveContractRepository = receiveContractRepository;
        }

        [HttpGet]
        [Route(ReceiveContractEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReceiveContract>))]
        public IActionResult GetReceiveContractById(int id)
        {
            if (!_receiveContractRepository.ReceiveContractExit(id))
                return NotFound();
            var receiveContract = _receiveContractRepository.GetReceiveContractById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(receiveContract);
        }

        [HttpGet]
        [Route(ReceiveContractEndpoints.GetByContractGroupId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReceiveContract>))]
        public IActionResult GetReceiveContractByContractGroupId(int contractGroupId)
        {
            var receiveContract = _receiveContractRepository.GetReceiveContractByContractGroupId(contractGroupId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(receiveContract);
        }

        [HttpPost]
        [Route(ReceiveContractEndpoints.Create)]
        public IActionResult CreateReceiveContract([FromBody] ReceiveContractCreateModel request)
        {
            _receiveContractRepository.CreateReceiveContract(request);
            return Ok();
        }

        [HttpPut]
        [Route(ReceiveContractEndpoints.Update)]
        public IActionResult Update(int id, [FromBody] ReceiveContractUpdateModel request)
        {
            if (request == null || id != request.Id)
                return BadRequest();

            // Check if the car with the specified id exists
            if (!_receiveContractRepository.ReceiveContractExit(id))
                return NotFound();

            // Update the car and its related data
            _receiveContractRepository.UpdateReceiveContract(id, request);

            return Ok();
        }

        [HttpPut]
        [Route(ReceiveContractEndpoints.UpdateContractStatus)]
        public IActionResult UpdateCarStatus([FromRoute] int id, [FromBody] ReceiveContractUpdateStatusModel request)
        {
            if (request == null || id != request.Id)
                return BadRequest();
            if (!_receiveContractRepository.ReceiveContractExit(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_receiveContractRepository.UpdateReceiveContractStatus(id, request))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}


  
