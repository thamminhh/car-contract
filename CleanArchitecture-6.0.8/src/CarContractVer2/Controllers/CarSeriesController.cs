using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarMake.Sub_Model;
using CleanArchitecture.Domain.Entities_SubModel.CarSeries.Sub_Model;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class CarSeriesController : ControllerBase
    {
        private readonly ICarSeriesRepository _carSeriesRepository;


        public CarSeriesController(ICarSeriesRepository carSeriesRepository)
        {
            _carSeriesRepository = carSeriesRepository;
        }

        [HttpGet]
        [Route(CarSeriesEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarSeries>))]
        public IActionResult GetCarSeriesById(int id)
        {
            if (!_carSeriesRepository.CarSeriesExit(id))
                return NotFound();
            var carSeries = _carSeriesRepository.GetCarSeriesById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carSeries);
        }

        [HttpGet]
        [Route(CarSeriesEndpoints.GetByCarModelIdAndCarGenerationId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarSeries>))]
        public IActionResult GetCarSeriesByCarId(int carModelId, int carGenerationId)
        {
            var carSeries = _carSeriesRepository.GetCarSeriesByCarModelAndCarGeneration(carModelId, carGenerationId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carSeries);
        }

        [HttpPut]
        [Route(CarSeriesEndpoints.Update)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCarSeries(int carSeriesId, [FromBody] CarSeriesUpdateModel request)
        {
            if (request == null || carSeriesId != request.Id)
                return BadRequest();

            // Check if the car with the specified id exists
            if (!_carSeriesRepository.CarSeriesExit(carSeriesId))
                return NotFound();

            // Update the car and its related data
            if (!_carSeriesRepository.UpdateCarSeries(carSeriesId, request, out string errorMessage))
            {
                ModelState.AddModelError("", errorMessage);
                return StatusCode(422, ModelState);
            }
            return Ok("Successfully Updated");
        }
    }
}


  
