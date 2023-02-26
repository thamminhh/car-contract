using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class ExpertiseContractController : ControllerBase
    {
        private readonly IExpertiseContractRepository _expertiseContractRepository;


        public ExpertiseContractController(IExpertiseContractRepository expertiseContractRepository)
        {
            _expertiseContractRepository = expertiseContractRepository;
        }

        [HttpGet]
        [Route(ExpertiseContractEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpertiseContract>))]
        public IActionResult GetExpertiseContractById(int id)
        {
            if (!_expertiseContractRepository.ExpertiseContractExit(id))
                return NotFound();
            var expertiseContract = _expertiseContractRepository.GetExpertiseContractById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(expertiseContract);
        }

        [HttpGet]
        [Route(ExpertiseContractEndpoints.GetByContractGroupId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpertiseContract>))]
        public IActionResult GetExpertiseContractByContractGroupId(int contractGroupId)
        {
            var expertiseContract = _expertiseContractRepository.GetExpertiseContractByContractGroupId(contractGroupId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(expertiseContract);
        }
    }
}


  
