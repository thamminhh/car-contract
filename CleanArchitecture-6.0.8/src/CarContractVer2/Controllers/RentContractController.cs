using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
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
    }
}


  
