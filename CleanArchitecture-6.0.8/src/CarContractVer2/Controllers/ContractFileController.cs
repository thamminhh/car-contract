using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class ContractFileController : ControllerBase
    {
        private readonly IContractFileRepository _contractFileRepository;


        public ContractFileController(IContractFileRepository contractFileRepository)
        {
            _contractFileRepository = contractFileRepository;
        }

        [HttpGet]
        [Route(ContractFileEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContractFile>))]
        public IActionResult GetContractFileById(int id)
        {
            if (!_contractFileRepository.ContractFileExit(id))
                return NotFound();
            var contractFile = _contractFileRepository.GetContractFileById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(contractFile);
        }

        [HttpGet]
        [Route(ContractFileEndpoints.GetByContractGroupId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContractFile>))]
        public IActionResult GetContractFileByContractGroupId(int contractGroupId)
        {
            var contractFile = _contractFileRepository.GetContractFileByContractGroupId(contractGroupId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(contractFile);
        }
    }
}


  
