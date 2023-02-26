using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
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
        //public IActionResult CreateCarMake([FromBody] CarMake carMakeCreate)
        //{
        //    if (carMakeCreate == null)
        //        return BadRequest(ModelState);

        //    var carMake = _carMakeController.GetCarMakes()
        //        .Where(c => c.Name.Trim().ToUpper() == carMakeCreate.Name.TrimEnd().ToUpper())
        //        .FirstOrDefault();
        //    if(carMake != null)
        //    {
        //        ModelState.AddModelError("", "Car Make Already Exist!!!");
        //        return StatusCode(422, ModelState);
        //    }
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    if (!_carMakeController.CreateCarMake(carMakeCreate))
        //    {
        //        ModelState.AddModelError("", "Something went wrong while saving");
        //        return StatusCode(500, ModelState);
        //    }
        //    return Ok("Successfully Added");
        //}

        [HttpPut]
        [Route(CarMakeEndpoints.Update)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCarMake(int id, [FromBody] CarMake carMakeUpdate)
        {
            if (carMakeUpdate == null)            
                return BadRequest(ModelState);
            
            if(id != carMakeUpdate.Id)
                return BadRequest(ModelState);

            if (!_carMakeController.CarMakeExit(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_carMakeController.UpdateCarMake(carMakeUpdate))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}


  
