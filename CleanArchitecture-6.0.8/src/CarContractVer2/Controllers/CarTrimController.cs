using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class CarTrimController : ControllerBase
    {
        private readonly ICarTrimRepository _carTrimRepository;


        public CarTrimController(ICarTrimRepository carTrimRepository)
        {
            _carTrimRepository = carTrimRepository;
        }

        [HttpGet]
        [Route(CarTrimEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarTrim>))]
        public IActionResult GetCarTrimById(int id)
        {
            if (!_carTrimRepository.CarTrimExit(id))
                return NotFound();
            var carTrim = _carTrimRepository.GetCarTrimById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carTrim);
        }

        [HttpGet]
        [Route(CarTrimEndpoints.GetByCarModelIdAndCarSeriesId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarTrim>))]
        public IActionResult GetCarTrimByCarId(int carModelId, int carSeriesId)
        {
            var carTrim = _carTrimRepository.GetCarTrimByCarModelAndCarSeries(carModelId, carSeriesId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carTrim);
        }
    }
}


  
