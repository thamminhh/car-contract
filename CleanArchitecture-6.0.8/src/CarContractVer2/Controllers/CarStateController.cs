using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using CleanArchitecture.Application.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class CarStateController : ControllerBase
    {
        private readonly ICarStateRepository _carStateRepository;


        public CarStateController(ICarStateRepository carStateRepository)
        {
            _carStateRepository = carStateRepository;
        }

        [HttpGet]
        [Route(CarStateEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarState>))]
        public IActionResult GetCarStateById(int id)
        {
            if (!_carStateRepository.CarStateExit(id))
                return NotFound();
            var carState = _carStateRepository.GetCarStateById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carState);
        }

        [HttpGet]
        [Route(CarStateEndpoints.GetByCarId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarState>))]
        public IActionResult GetCarStateByCarId(int carId)
        {
            var carState = _carStateRepository.GetCarStateByCarId(carId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carState);
        }
    }
}


  
