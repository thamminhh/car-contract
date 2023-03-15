using CleanArchitecture.Application.Repository;
using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.CarRegistryInfo;
using CleanArchitecture.Domain.Entities_SubModel.CarRegistryInfo.SubModel;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]

    public class CarRegistryInfoController : ControllerBase
    {
        private readonly ICarRegistryInfoRepository _carRegistryInfoRepository;


        public CarRegistryInfoController(ICarRegistryInfoRepository carRegistryInfoRepository)
        {
            _carRegistryInfoRepository = carRegistryInfoRepository;
        }

        [HttpGet]
        [Route(CarRegistryInfoEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarRegistryInfo>))]
        public IActionResult GetCarRegistryInfoById(int id)
        {
            if (!_carRegistryInfoRepository.CarRegistryInfoExit(id))
                return NotFound();
            var CarRegistryInfo = _carRegistryInfoRepository.GetCarRegistryInfoById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(CarRegistryInfo);
        }

        [HttpGet]
        [Route(CarRegistryInfoEndpoints.GetByCarId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarRegistryInfo>))]
        public IActionResult GetCarRegistryInfoByContractGroupId(int carId)
        {
            var CarRegistryInfo = _carRegistryInfoRepository.GetCarRegistryInfoByCarId(carId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(CarRegistryInfo);
        }

        [HttpPost]
        [Route(CarRegistryInfoEndpoints.Create)]
        public IActionResult CreateCarRegistryInfo([FromBody] CarRegistryInfoCreateModel request)
        {
            if (request == null)
                return BadRequest(ModelState);

            _carRegistryInfoRepository.CreateCarRegistryInfo(request);
            return Ok();
        }

        [HttpPut]
        [Route(CarRegistryInfoEndpoints.Update)]
        public IActionResult Update(int id, [FromBody] CarRegistryInfo request)
        {
            if (request == null || id != request.Id)
                return BadRequest();

            // Check if the car with the specified id exists
            if (!_carRegistryInfoRepository.CarRegistryInfoExit(id))
                return NotFound();

            // Update the car and its related data
            _carRegistryInfoRepository.UpdateCarRegistryInfo(id, request);

            return Ok();
        }

    }
}



