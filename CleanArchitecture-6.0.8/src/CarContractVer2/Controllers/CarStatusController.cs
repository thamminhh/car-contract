using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]

    public class CarStatusController : ControllerBase
    {
        private readonly ICarStatusRepository _carStatusController;


        public CarStatusController(ICarStatusRepository carStatusController)
        {
            _carStatusController = carStatusController;
        }

        [HttpGet]
        [Route(CarStatusEndpoints.GetAll)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarStatus>))]
        public IActionResult GetCarStatuss()
        {
            var listCarStatus = _carStatusController.GetCarStatuss();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(listCarStatus);
        }

        [HttpGet]
        [Route(CarStatusEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarStatus>))]
        public IActionResult GetCarStatusById(int id)
        {
            if (!_carStatusController.CarStatusExit(id))
                return NotFound();
            var carStatus = _carStatusController.GetCarStatusById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carStatus);
        }

        [HttpPost]
        [Route(CarStatusEndpoints.Create)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCarStatus([FromBody] CarStatus carStatusCreate)
        {
            if (carStatusCreate == null)
                return BadRequest(ModelState);

            var carStatus = _carStatusController.GetCarStatuss()
                .Where(c => c.Name.Trim().ToUpper() == carStatusCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            if(carStatus != null)
            {
                ModelState.AddModelError("", "ParKing Lot Already Exist!!!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_carStatusController.CreateCarStatus(carStatusCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Added");
        }

        [HttpPut]
        [Route(CarStatusEndpoints.Update)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCarStatus(int id, [FromBody] CarStatus carStatusUpdate)
        {
            if (carStatusUpdate == null)            
                return BadRequest(ModelState);
            
            if(id != carStatusUpdate.Id)
                return BadRequest(ModelState);

            if (!_carStatusController.CarStatusExit(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_carStatusController.UpdateCarStatus(carStatusUpdate))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}


  
