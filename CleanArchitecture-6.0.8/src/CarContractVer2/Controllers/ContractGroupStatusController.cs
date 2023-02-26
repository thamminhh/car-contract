using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]

    public class ContractGroupStatusController : ControllerBase
    {
        private readonly IContractGroupStatusRepository _contractGroupStatusController;


        public ContractGroupStatusController(IContractGroupStatusRepository contractGroupStatusController)
        {
            _contractGroupStatusController = contractGroupStatusController;
        }

        [HttpGet]
        [Route(ContractGroupStatusEndpoints.GetAll)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContractGroupStatus>))]
        public IActionResult GetContractGroupStatuss()
        {
            var listContractGroupStatus = _contractGroupStatusController.GetContractGroupStatuss();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(listContractGroupStatus);
        }

        [HttpGet]
        [Route(ContractGroupStatusEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContractGroupStatus>))]
        public IActionResult GetContractGroupStatusById(int id)
        {
            if (!_contractGroupStatusController.ContractGroupStatusExit(id))
                return NotFound();
            var ContractGroupStatus = _contractGroupStatusController.GetContractGroupStatusById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(ContractGroupStatus);
        }

        //[HttpPost]
        //[Route(ContractGroupStatusEndpoints.Create)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult CreateContractGroupStatus([FromBody] ContractGroupStatus contractGroupStatusCreate)
        //{
        //    if (contractGroupStatusCreate == null)
        //        return BadRequest(ModelState);

        //    var contractGroupStatus = _contractGroupStatusController.GetContractGroupStatuss()
        //        .Where(c => c.Name.Trim().ToUpper() == contractGroupStatusCreate.Name.TrimEnd().ToUpper())
        //        .FirstOrDefault();
        //    if(contractGroupStatus != null)
        //    {
        //        ModelState.AddModelError("", "ParKing Lot Already Exist!!!");
        //        return StatusCode(422, ModelState);
        //    }
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    if (!_contractGroupStatusController.CreateContractGroupStatus(contractGroupStatusCreate))
        //    {
        //        ModelState.AddModelError("", "Something went wrong while saving");
        //        return StatusCode(500, ModelState);
        //    }
        //    return Ok("Successfully Added");
        //}

        [HttpPut]
        [Route(ContractGroupStatusEndpoints.Update)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateContractGroupStatus(int id, [FromBody] ContractGroupStatus contractGroupStatusUpdate)
        {
            if (contractGroupStatusUpdate == null)            
                return BadRequest(ModelState);
            
            if(id != contractGroupStatusUpdate.Id)
                return BadRequest(ModelState);

            if (!_contractGroupStatusController.ContractGroupStatusExit(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_contractGroupStatusController.UpdateContractGroupStatus(contractGroupStatusUpdate))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}


  
