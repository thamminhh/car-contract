using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class ForControlController : ControllerBase
    {
        private readonly IForControlRepository _forControlRepository;


        public ForControlController(IForControlRepository forControlRepository)
        {
            _forControlRepository = forControlRepository;
        }

        [HttpGet]
        [Route(ForControlEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ForControl>))]
        public IActionResult GetForControlById(int id)
        {
            if (!_forControlRepository.ForControlExit(id))
                return NotFound();
            var forControl = _forControlRepository.GetForControlById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(forControl);
        }

        [HttpGet]
        [Route(ForControlEndpoints.GetByCarId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ForControl>))]
        public IActionResult GetForControlByCarId(int carId)
        {
            var forControl = _forControlRepository.GetForControlByCarId(carId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(forControl);
        }
    }
}


  
