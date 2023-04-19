using CleanArchitecture.Application.Repository;
using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]

    public class StatisticController : ControllerBase
    {
        private readonly IContractStatisticRepository _contractStatisticRepository;
        private readonly ICarRepository _carRepository;

        public StatisticController(IContractStatisticRepository contractStatisticRepository, ICarRepository carRepository)
        {
            _contractStatisticRepository = contractStatisticRepository;
            _carRepository = carRepository;
        }

        [HttpGet]
        [Route(StatisticEndpoints.GetByContractGroup)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContractStatistic>))]
        public IActionResult GetContractStatistics([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var listContractStatistic = _contractStatisticRepository.GetContractStatistic(from, to);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(listContractStatistic);
        }

        [HttpGet]
        [Route(StatisticEndpoints.GetByCar)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContractStatistic>))]
        public IActionResult GetCarStatistics([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var listCarStatistic = _carRepository.GetCarStatistic(from, to);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(listCarStatistic);
        }
    }
}


  
