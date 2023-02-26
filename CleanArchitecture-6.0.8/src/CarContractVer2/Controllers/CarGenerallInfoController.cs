using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class CarGenerallInfoController : ControllerBase
    {
        private readonly ICarGenerallInfoRepository _carGenerallInfoRepository;


        public CarGenerallInfoController(ICarGenerallInfoRepository carGenerallInfoRepository)
        {
            _carGenerallInfoRepository = carGenerallInfoRepository;
        }

        [HttpGet]
        [Route(CarGenerallInfoEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarGenerallInfo>))]
        public IActionResult GetCarGenerallInfoById(int id)
        {
            if (!_carGenerallInfoRepository.CarGenerallInfoExit(id))
                return NotFound();
            var carGenerallInfo = _carGenerallInfoRepository.GetCarGenerallInfoById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carGenerallInfo);
        }

        [HttpGet]
        [Route(CarGenerallInfoEndpoints.GetByCarId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarGenerallInfo>))]
        public IActionResult GetCarGenerallInfoByCarId(int carId)
        {
            var carGenerallInfo = _carGenerallInfoRepository.GetCarGenerallInfoByCarId(carId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carGenerallInfo);
        }
    }
}


  
