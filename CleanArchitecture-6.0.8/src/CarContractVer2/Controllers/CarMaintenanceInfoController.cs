using CleanArchitecture.Application.Repository;
using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.CarMaintenanceInfo;
using CleanArchitecture.Domain.Entities_SubModel.CarMaintenanceInfo.SubModel;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]

    public class CarMaintenanceInfoController : ControllerBase
    {
        private readonly ICarMaintenanceInfoRepository _carMaintenanceInfoRepository;


        public CarMaintenanceInfoController(ICarMaintenanceInfoRepository carMaintenanceInfoRepository)
        {
            _carMaintenanceInfoRepository = carMaintenanceInfoRepository;
        }

        [HttpGet]
        [Route(CarMaintenanceInfoEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarMaintenanceInfo>))]
        public IActionResult GetCarMaintenanceInfoById(int id)
        {
            if (!_carMaintenanceInfoRepository.CarMaintenanceInfoExit(id))
                return NotFound();
            var CarMaintenanceInfo = _carMaintenanceInfoRepository.GetCarMaintenanceInfoById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(CarMaintenanceInfo);
        }

        [HttpGet]
        [Route(CarMaintenanceInfoEndpoints.GetByCarId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarMaintenanceInfo>))]
        public IActionResult GetCarMaintenanceInfoByContractGroupId(int carId)
        {
            var carMaintenanceInfo = _carMaintenanceInfoRepository.GetCarMaintenanceInfoByCarId(carId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carMaintenanceInfo);
        }

        [HttpPost]
        [Route(CarMaintenanceInfoEndpoints.Create)]
        public IActionResult CreateCarMaintenanceInfo([FromBody] CarMaintenanceInfoCreateModel request)
        {
            if (request == null)
                return BadRequest(ModelState);

            _carMaintenanceInfoRepository.CreateCarMaintenanceInfo(request);
            return Ok();
        }

        [HttpPut]
        [Route(CarMaintenanceInfoEndpoints.Update)]
        public IActionResult Update(int id, [FromBody] CarMaintenanceInfo request)
        {
            if (request == null || id != request.Id)
                return BadRequest();

            // Check if the car with the specified id exists
            if (!_carMaintenanceInfoRepository.CarMaintenanceInfoExit(id))
                return NotFound();

            // Update the car and its related data
            _carMaintenanceInfoRepository.UpdateCarMaintenanceInfo(id, request);

            return Ok();
        }

    }
}



