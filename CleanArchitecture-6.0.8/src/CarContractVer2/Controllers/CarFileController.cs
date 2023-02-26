using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class CarFileController : ControllerBase
    {
        private readonly ICarFileRepository _carFileRepository;


        public CarFileController(ICarFileRepository carFileRepository)
        {
            _carFileRepository = carFileRepository;
        }

        [HttpGet]
        [Route(CarFileEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarFile>))]
        public IActionResult GetCarFileById(int id)
        {
            if (!_carFileRepository.CarFileExit(id))
                return NotFound();
            var carFile = _carFileRepository.GetCarFileById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carFile);
        }

        [HttpGet]
        [Route(CarFileEndpoints.GetByCarId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarFile>))]
        public IActionResult GetCarFileByCarId(int carId)
        {
            var carFile = _carFileRepository.GetCarFileByCarId(carId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carFile);
        }
    }
}


  
