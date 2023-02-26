using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
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
    }
}


  
