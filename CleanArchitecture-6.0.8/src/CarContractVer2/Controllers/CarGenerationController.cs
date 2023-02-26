using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class CarGenerationController : ControllerBase
    {
        private readonly ICarGenerationRepository _carGenerationRepository;


        public CarGenerationController(ICarGenerationRepository carGenerationRepository)
        {
            _carGenerationRepository = carGenerationRepository;
        }

        [HttpGet]
        [Route(CarGenerationEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarGeneration>))]
        public IActionResult GetCarGenerationById(int id)
        {
            if (!_carGenerationRepository.CarGenerationExit(id))
                return NotFound();
            var carGeneration = _carGenerationRepository.GetCarGenerationById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carGeneration);
        }

        [HttpGet]
        [Route(CarGenerationEndpoints.GetByCarModelId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarGeneration>))]
        public IActionResult GetCarGenerationByCarId(int carModelId)
        {
            var carGeneration = _carGenerationRepository.GetCarGenerationByCarModelId(carModelId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carGeneration);
        }
    }
}


  
