using CleanArchitecture.Application.Repository;
using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]

    public class ContractStatisticController : ControllerBase
    {
        private readonly IContractStatisticRepository _ContractStatisticRepository;

        public ContractStatisticController(IContractStatisticRepository ContractStatisticRepository)
        {
            _ContractStatisticRepository = ContractStatisticRepository;
        }

        [HttpGet]
        [Route(ContractStatisticEndpoints.GetList)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContractStatistic>))]
        public IActionResult GetContractStatistics([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var listContractStatistic = _ContractStatisticRepository.GetContractStatistic(from, to);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(listContractStatistic);
        }

    }
}


  
