using CleanArchitecture.Application.Repository;
using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.User.SubModel;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarMakeRepository _carMakeController;


        public CarController(ICarRepository carRepository, ICarMakeRepository carMakeController
            , IWebHostEnvironment webHostEnvironment
            )
        {
            _carRepository = carRepository;
            _carMakeController = carMakeController;

        }

        [HttpGet]
        [Route(CarEndpoints.GetAll)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetCars([FromQuery] CarFilter filter, int page = 1, int pageSize = 10)
        {
            var listCar = _carRepository.GetCars(page, pageSize, filter);
            var count = _carRepository.GetNumberOfCars(filter);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(new { cars = listCar, total = count }); 
        }

        [HttpGet]
        [Route(CarEndpoints.GetAllActive)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetCarsActive([FromQuery] CarFilter filter,int page = 1, int pageSize = 10)
        {
            var listCar = _carRepository.GetCarsActive(page, pageSize, filter);
            var count = _carRepository.GetNumberOfCarsActive(filter);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(new { cars = listCar, total = count });
        }

        [HttpGet]
        [Route(CarEndpoints.GetCarsMaintenance)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetCarsMaintenance(int page = 1, int pageSize = 10)
        {
            var listCar = _carRepository.GetCarsMaintenance(page, pageSize, out int count);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(new { cars = listCar, total = count });
        }

        [HttpGet]
        [Route(CarEndpoints.GetCarsRegistry)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetCarsRegistry(int page = 1, int pageSize = 10)
        {
            var listCar = _carRepository.GetCarsRegistry(page, pageSize, out int count);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(new { cars = listCar, total = count });
        }

        //[HttpGet]
        //[Route(CarEndpoints.GetByStatus)]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        //public IActionResult GetCarsByStatusId( [FromQuery] int carStatusId, int page = 1, int pageSize = 10)
        //{
        //    var listCar = _carRepository.GetCarsByStatusId(page, pageSize, carStatusId);
        //    var count = _carRepository.GetNumberOfCarsByStatusId(carStatusId);
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    return Ok(new { cars = listCar, total = count });
        //}

        //[HttpGet]
        //[Route(CarEndpoints.GetByCarMakeName)]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        //public IActionResult GetCarsByCarMakeName([FromQuery] string carMakeName, int page = 1, int pageSize = 10)
        //{
        //    int carMakeId = _carMakeController.GetCarMakeIdByName(carMakeName);
        //    var listCar = _carRepository.GetCarsByCarMakeId(page, pageSize, carMakeId);
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    return Ok(listCar);
        //}

        [HttpGet]
        [Route(CarEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetCarById(int carId)
        {
            if (!_carRepository.CarExit(carId))
                return NotFound();
            var car = _carRepository.GetCarById(carId);

            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }


        [HttpPost]
        [Route(CarEndpoints.Create)]
        public IActionResult CreateCar([FromBody] CarCreateModel request)
        {
            if (request == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_carRepository.CreateCar(request, out string errorMessage))
            {
                ModelState.AddModelError("", errorMessage);
                return StatusCode(422, ModelState);
            }
            return Ok("Successfully Added");
        }

        [HttpPut]
        [Route(CarEndpoints.Update)]
        public IActionResult Update(int id, [FromBody] CarUpdateModel request)
        {
            if (request == null || id != request.Id)
                return BadRequest();

            // Check if the car with the specified id exists
            if (!_carRepository.CarExit(id))
                return NotFound();

            // Update the car and its related data
            _carRepository.UpdateCar(id, request);

            return Ok();
        }

        [HttpPut]
        [Route(CarEndpoints.UpdateCarStatus)]
        public IActionResult UpdateCarStatus([FromRoute]int id, [FromBody] CarUpdateStatusModel request)
        {
            if (request == null || id != request.id)
                return BadRequest();
            if (!_carRepository.CarExit(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if(!_carRepository.UpdateCarStatus(id, request))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPut]
        [Route(CarEndpoints.Delete)]
        public IActionResult DeleteCar([FromRoute] int id)
        {
            if (!_carRepository.CarExit(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_carRepository.DeleteCar(id))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        //[HttpGet("root")]
        //public IActionResult Get()
        //{
        //    string rootPath = _webHostEnvironment.ContentRootPath;
        //    var localhost = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
        //    return Ok(new { rootPath,localhost });
        //}
    }

}


  
