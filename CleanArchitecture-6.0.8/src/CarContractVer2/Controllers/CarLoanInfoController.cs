using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class CarLoanInfoController : ControllerBase
    {
        private readonly ICarLoanInfoRepository _carLoanInfoRepository;


        public CarLoanInfoController(ICarLoanInfoRepository carLoanInfoRepository)
        {
            _carLoanInfoRepository = carLoanInfoRepository;
        }

        [HttpGet]
        [Route(CarLoanInfoEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarLoanInfo>))]
        public IActionResult GetCarLoanInfoById(int id)
        {
            if (!_carLoanInfoRepository.CarLoanInfoExit(id))
                return NotFound();
            var carLoanInfo = _carLoanInfoRepository.GetCarLoanInfoById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carLoanInfo);
        }

        [HttpGet]
        [Route(CarLoanInfoEndpoints.GetByCarId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarLoanInfo>))]
        public IActionResult GetCarLoanInfoByCarId(int carId)
        {
            var carLoanInfo = _carLoanInfoRepository.GetCarLoanInfoByCarId(carId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carLoanInfo);
        }
    }
}


  
