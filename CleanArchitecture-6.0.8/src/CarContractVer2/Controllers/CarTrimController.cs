using CleanArchitecture.Application.Repository;
using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarSeries.Sub_Model;
using CleanArchitecture.Domain.Entities_SubModel.CarTrim.Sub_Model;
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
        public IActionResult GetCarTrimByCarModelIdAndCarSeriesId(int carModelId, int carSeriesId)
        {
            var carTrim = _carTrimRepository.GetCarTrimByCarModelAndCarSeries(carModelId, carSeriesId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carTrim);
        }

        [HttpGet]
        [Route(CarTrimEndpoints.GetByCarModelId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarTrim>))]
        public IActionResult GetCarTrimByCarModelId(int carModelId)
        {
            var carTrim = _carTrimRepository.GetCarTrimByCarModelId(carModelId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carTrim);
        }

        [HttpPut]
        [Route(CarTrimEndpoints.Update)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCarTrim(int carTrimId, [FromBody] CarTrimUpdateModel request)
        {
            if (request == null || carTrimId != request.Id)
                return BadRequest();

            // Check if the car with the specified id exists
            if (!_carTrimRepository.CarTrimExit(carTrimId))
                return NotFound();

            // Update the car and its related data
            if (!_carTrimRepository.UpdateCarTrim(carTrimId, request, out string errorMessage))
            {
                ModelState.AddModelError("", errorMessage);
                return StatusCode(422, ModelState);
            }
            return Ok("Successfully Updated");
        }
    }
}


  
