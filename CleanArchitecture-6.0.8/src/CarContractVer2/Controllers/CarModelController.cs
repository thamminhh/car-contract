using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarMake.Sub_Model;
using CleanArchitecture.Domain.Entities_SubModel.CarModel.Sub_Model;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class CarModelController : ControllerBase
    {
        private readonly ICarModelRepository _carModelRepository;


        public CarModelController(ICarModelRepository carModelRepository)
        {
            _carModelRepository = carModelRepository;
        }

        [HttpGet]
        [Route(CarModelEndpoints.GetAll)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarModel>))]
        public IActionResult GetCarModels()
        {
            var carModels = _carModelRepository.GetCarModels();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carModels);
        }


        [HttpGet]
        [Route(CarModelEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarModel>))]
        public IActionResult GetCarModelById(int id)
        {
            if (!_carModelRepository.CarModelExit(id))
                return NotFound();
            var carModel = _carModelRepository.GetCarModelById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carModel);
        }

        [HttpGet]
        [Route(CarModelEndpoints.GetByCarMakeId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarModel>))]
        public IActionResult GetCarModelByCarId(int carMakeId)
        {
            var carModel = _carModelRepository.GetCarModelsByCarMakeId(carMakeId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carModel);
        }

        [HttpPut]
        [Route(CarModelEndpoints.Update)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCarModel(int carModelId, [FromBody] CarModelUpdateModel request)
        {
            if (request == null || carModelId != request.Id)
                return BadRequest();

            // Check if the car with the specified id exists
            if (!_carModelRepository.CarModelExit(carModelId))
                return NotFound();

            // Update the car and its related data
            if (!_carModelRepository.UpdateCarModel(carModelId, request, out string errorMessage))
            {
                ModelState.AddModelError("", errorMessage);
                return StatusCode(422, ModelState);
            }
            return Ok("Successfully Updated");
        }
    }
}


  
