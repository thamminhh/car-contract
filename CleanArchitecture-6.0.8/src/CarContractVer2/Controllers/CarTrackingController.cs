using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class CarTrackingController : ControllerBase
    {
        private readonly ICarTrackingRepository _carTrackingRepository;


        public CarTrackingController(ICarTrackingRepository carTrackingRepository)
        {
            _carTrackingRepository = carTrackingRepository;
        }

        [HttpGet]
        [Route(CarTrackingEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarTracking>))]
        public IActionResult GetCarTrackingById(int id)
        {
            if (!_carTrackingRepository.CarTrackingExit(id))
                return NotFound();
            var carTracking = _carTrackingRepository.GetCarTrackingById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carTracking);
        }

        [HttpGet]
        [Route(CarTrackingEndpoints.GetByCarId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarTracking>))]
        public IActionResult GetCarTrackingByCarId(int carId)
        {
            var carTracking = _carTrackingRepository.GetCarTrackingByCarId(carId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carTracking);
        }
    }
}


  
