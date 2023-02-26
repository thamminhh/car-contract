using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]

    public class ParkingLotController : ControllerBase
    {
        private readonly IParkingLotRepository _parkingLotController;


        public ParkingLotController(IParkingLotRepository parkingLotController)
        {
            _parkingLotController = parkingLotController;
        }

        [HttpGet]
        [Route(ParkingLotEndpoints.GetAll)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ParkingLot>))]
        public IActionResult GetParkingLots()
        {
            var listParkingLot = _parkingLotController.GetParkingLots();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(listParkingLot);
        }

        [HttpGet]
        [Route(ParkingLotEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ParkingLot>))]
        public IActionResult GetParkingLotById(int id)
        {
            if (!_parkingLotController.ParkingLotExit(id))
                return NotFound();
            var parkingLot = _parkingLotController.GetParkingLotById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(parkingLot);
        }

        [HttpPost]
        [Route(ParkingLotEndpoints.Create)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateParkingLot([FromBody] ParkingLot ParkingLotCreate)
        {
            if (ParkingLotCreate == null)
                return BadRequest(ModelState);

            var parkingLot = _parkingLotController.GetParkingLots()
                .Where(c => c.Name.Trim().ToUpper() == ParkingLotCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            if(parkingLot != null)
            {
                ModelState.AddModelError("", "ParKing Lot Already Exist!!!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_parkingLotController.CreateParkingLot(ParkingLotCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Added");
        }

        [HttpPut]
        [Route(ParkingLotEndpoints.Update)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateParkingLot(int id, [FromBody] ParkingLot ParkingLotUpdate)
        {
            if (ParkingLotUpdate == null)            
                return BadRequest(ModelState);
            
            if(id != ParkingLotUpdate.Id)
                return BadRequest(ModelState);

            if (!_parkingLotController.ParkingLotExit(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_parkingLotController.UpdateParkingLot(ParkingLotUpdate))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}


  
