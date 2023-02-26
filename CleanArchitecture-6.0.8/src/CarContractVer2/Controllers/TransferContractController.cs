using CleanArchitecture.Application.Repository;
using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.TransferContract;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class TransferContractController : ControllerBase
    {
        private readonly ITransferContractRepository _transferContractRepository;


        public TransferContractController(ITransferContractRepository transferContractRepository)
        {
            _transferContractRepository = transferContractRepository;
        }

        [HttpGet]
        [Route(TransferContractEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransferContract>))]
        public IActionResult GetTransferContractById(int id)
        {
            if (!_transferContractRepository.TransferContractExit(id))
                return NotFound();
            var transferContract = _transferContractRepository.GetTransferContractById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(transferContract);
        }

        [HttpGet]
        [Route(TransferContractEndpoints.GetByContractGroupId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransferContract>))]
        public IActionResult GetTransferContractByContractGroupId(int contractGroupId)
        {
            var TransferContract = _transferContractRepository.GetTransferContractByContractGroupId(contractGroupId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(TransferContract);
        }

        [HttpPost]
        [Route(TransferContractEndpoints.Create)]
        public IActionResult CreateTransferContract([FromBody] TransferContractCreateModel request)
        {
            _transferContractRepository.CreateExpertiesContract(request);
            return Ok();
        }

        [HttpPut]
        [Route(TransferContractEndpoints.Update)]
        public IActionResult Update(int id, [FromBody] TransferContractUpdateModel request)
        {
            if (request == null || id != request.Id)
                return BadRequest();

            // Check if the car with the specified id exists
            if (!_transferContractRepository.TransferContractExit(id))
                return NotFound();

            // Update the car and its related data
            _transferContractRepository.UpdateTransferContract(id, request);

            return Ok();
        }

        [HttpPut]
        [Route(TransferContractEndpoints.UpdateContractStatus)]
        public IActionResult UpdateCarStatus([FromRoute] int id, [FromBody] TransferContractUpdateStatusModel request)
        {
            if (request == null || id != request.Id)
                return BadRequest();
            if (!_transferContractRepository.TransferContractExit(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_transferContractRepository.UpdateTransferContractStatus(id, request))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}


  
