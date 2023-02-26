using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
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
    }
}


  
