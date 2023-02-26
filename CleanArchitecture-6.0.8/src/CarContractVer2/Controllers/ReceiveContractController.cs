using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
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
    }
}


  
