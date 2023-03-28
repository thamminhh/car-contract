using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarGeneration.Sub_Model;
using CleanArchitecture.Domain.Entities_SubModel.CarMake.Sub_Model;
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

        [HttpPut]
        [Route(CarGenerationEndpoints.Update)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCarMake(int carGenerationId, [FromBody] CarGenerationUpdateModel request)
        {
            if (request == null || carGenerationId != request.Id)
                return BadRequest();

            // Check if the car with the specified id exists
            if (!_carGenerationRepository.CarGenerationExit(carGenerationId))
                return NotFound();

            // Update the car and its related data
            if (!_carGenerationRepository.UpdateCarGeneration(carGenerationId, request, out string errorMessage))
            {
                ModelState.AddModelError("", errorMessage);
                return StatusCode(422, ModelState);
            }
            return Ok("Successfully Updated");
        }
    }
}


  
