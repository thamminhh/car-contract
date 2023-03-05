using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarSchedule;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]

    public class CarScheduleController : ControllerBase
    {
        private readonly ICarScheduleRepository _carScheduleRepository;

        public CarScheduleController(ICarScheduleRepository carScheduleRepository)
        {
            _carScheduleRepository = carScheduleRepository;
        }

        [HttpGet]
        [Route(CarScheduleEndpoints.GetAll)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarSchedule>))]
        public IActionResult GetCarSchedules()
        {
            var listCarSchedule = _carScheduleRepository.GetCarSchedules();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(listCarSchedule);
        }

        [HttpGet]
        [Route(CarScheduleEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarSchedule>))]
        public IActionResult GetCarScheduleById(int carScheduleId)
        {
            var carSchedule = _carScheduleRepository.GetCarScheduleById(carScheduleId);

            if (carSchedule == null)
            {
                return NotFound();
            }
            return Ok(carSchedule);
        }

        [HttpGet]
        [Route(CarScheduleEndpoints.GetByCarId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarSchedule>))]
        public IActionResult GetCarScheduleByCarId(int carId)
        {
            var carSchedule = _carScheduleRepository.GetCarSchedulesByCarId(carId);

            if (carSchedule == null)
            {
                return NotFound();
            }
            return Ok(carSchedule);
        }

        [HttpGet]
        [Route(CarScheduleEndpoints.GetByCarStatusId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarSchedule>))]
        public IActionResult GetCarScheduleByCarStatusId(int carStatusId)
        {
            var carSchedule = _carScheduleRepository.GetCarSchedulesStatusId(carStatusId);

            if (carSchedule == null)
            {
                return NotFound();
            }
            return Ok(carSchedule);
        }


        [HttpPost]
        [Route(CarScheduleEndpoints.Create)]
        public IActionResult CreateCarSchedule([FromBody] CarScheduleCreateModel request)
        {
            _carScheduleRepository.CreateCarSchedule(request);
            return Ok();
        }

        [HttpPut]
        [Route(CarScheduleEndpoints.Update)]
        public IActionResult Update(int id, [FromBody] CarScheduleUpdateModel request)
        {
            if (request == null || id != request.Id)
                return BadRequest();

            // Update the CarSchedule and its related data
            _carScheduleRepository.UpdateCarSchedule(id, request);

            return Ok();
        }

    }
}


  
