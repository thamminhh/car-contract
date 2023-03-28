using CleanArchitecture.Application.Repository;
using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarMake.Sub_Model;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]

    public class CarMakeController : ControllerBase
    {
        private readonly ICarMakeRepository _carMakeController;


        public CarMakeController(ICarMakeRepository carMakeController)
        {
            _carMakeController = carMakeController;
        }

        [HttpGet]
        [Route(CarMakeEndpoints.GetAll)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarMake>))]
        public IActionResult GetCarMakes()
        {
            var listCarMake = _carMakeController.GetCarMakes();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(listCarMake);
        }

        [HttpGet]
        [Route(CarMakeEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarMake>))]
        public IActionResult GetCarMakeById(int id)
        {
            if (!_carMakeController.CarMakeExit(id))
                return NotFound();
            var carMake = _carMakeController.GetCarMakeById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carMake);
        }

        //[HttpPost]
        //[Route(CarMakeEndpoints.Create)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult CreateCarMake([FromBody] CarMakeCreateModel request)
        //{
        //    if (request == null)
        //        return BadRequest(ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (!_carMakeController.CreateCarMake(request, out string errorMessage))
        //    {
        //        ModelState.AddModelError("", errorMessage);
        //        return StatusCode(422, ModelState);
        //    }
        //    return Ok("Successfully Added");
        //}

        [HttpPut]
        [Route(CarMakeEndpoints.Update)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCarMake(int carMakeId, [FromBody] CarMakeUpdateModel request)
        {
            if (request == null || carMakeId != request.Id)
                return BadRequest();

            // Check if the car with the specified id exists
            if (!_carMakeController.CarMakeExit(carMakeId))
                return NotFound();

            // Update the car and its related data
            if (!_carMakeController.UpdateCarMake(carMakeId, request, out string errorMessage))
            {
                ModelState.AddModelError("", errorMessage);
                return StatusCode(422, ModelState);
            }
            return Ok("Successfully Updated");
        }
    }
}


  
