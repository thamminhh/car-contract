using CleanArchitecture.Application.Repository;
using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarExpense.Sub_Model;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]

    public class CarExpenseController : ControllerBase
    {
        private readonly ICarExpenseRepository _carExpenseRepository;

        public CarExpenseController(ICarExpenseRepository carExpenseRepository)
        {
            _carExpenseRepository = carExpenseRepository;
        }

        [HttpGet]
        [Route(CarExpenseEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarExpense>))]
        public IActionResult GetCarExpenseById(int carExpenseId)
        {
            var carExpense = _carExpenseRepository.GetCarExpenseById(carExpenseId);

            if (carExpense == null)
            {
                return NotFound();
            }
            return Ok(carExpense);
        }

        [HttpGet]
        [Route(CarExpenseEndpoints.GetByCarId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarExpense>))]
        public IActionResult GetCarExpenseByCarId(int carId)
        {
            var carExpense = _carExpenseRepository.GetCarExpensesByCarId(carId);

            if (carExpense == null)
            {
                return NotFound();
            }
            return Ok(carExpense);
        }


        [HttpPost]
        [Route(CarExpenseEndpoints.Create)]
        public IActionResult CreateCarExpense([FromBody] CarExpenseCreateModel request)
        {
            _carExpenseRepository.CreateCarExpense(request);
            return Ok();
        }

        [HttpPut]
        [Route(CarExpenseEndpoints.Update)]
        public IActionResult Update(int id, [FromBody] CarExpenseUpdateModel request)
        {
            if (request == null || id != request.Id)
                return BadRequest();

            // Update the CarExpense and its related data
            _carExpenseRepository.UpdateCarExpense(id, request);

            return Ok();
        }

        [HttpDelete]
        [Route(CarExpenseEndpoints.Delete)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(int carExpenseId)
        {
            bool deleted = await _carExpenseRepository.DeleteCarExpense(carExpenseId);

            if (deleted)
            {
                return Ok("Deleted"); // Object deleted successfully
            }
            return NotFound(); // Object not found
        }

    }
}


  
