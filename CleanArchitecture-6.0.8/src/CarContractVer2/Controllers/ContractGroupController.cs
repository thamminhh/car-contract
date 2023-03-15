using CleanArchitecture.Application.Hubs;
using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class ContractGroupController : ControllerBase
    {
        private readonly IContractGroupRepository _contractGroupRepository;
        private readonly Microsoft.AspNetCore.SignalR.IHubContext<ContractGroupHub> _hubContract;

        public ContractGroupController(IContractGroupRepository contractGroupRepository, Microsoft.AspNetCore.SignalR.IHubContext<ContractGroupHub> hubContract)
        {
            _contractGroupRepository = contractGroupRepository;
            _hubContract = hubContract;
 
        }

        [HttpGet]
        [Route(ContractGroupEndpoints.GetAll)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContractGroup>))]
        public IActionResult GetContractGroups([FromQuery] ContractFilter filter,int page = 1, int pageSize = 10)
        {
            var count = _contractGroupRepository.GetNumberOfContracts(filter);
            var listContractGroup = _contractGroupRepository.GetContractGroups(page, pageSize, filter);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(new { contracts = listContractGroup, total = count });
        }
        //[HttpGet]
        //[Route(ContractGroupEndpoints.GetSingle)]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<ContractGroup>))]
        //public IActionResult GetContractGroupById(int contractGroupId)
        //{
        //    {
        //        if (!_contractGroupRepository.ContractGroupExit(contractGroupId))
        //            return NotFound();

        //        var contractGroup = _contractGroupRepository.GetContractGroupById(contractGroupId);
        //        //var connectionId = Request.Query["connectionId"]; // extract connection ID from query parameter

        //        if (contractGroup == null)
        //        {
        //            return NotFound();
        //        }
        //        //var contractGroupData = contractGroup;

        //        //var hubContext = GlobalHost.ConnectionManager.GetHubContext<ContractGroupHub>();
        //        //hubContext.Clients.Client(connectionId).ContractGroupUpdated(contractGroup);

        //        return Ok(contractGroup);

        //    }
        //}

        [HttpGet]
        [Route(ContractGroupEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContractGroup>))]
        public async Task<IActionResult> GetContractGroupById(int contractGroupId)
        {
            if (!_contractGroupRepository.ContractGroupExit(contractGroupId))
                return NotFound();

            var contractGroup = _contractGroupRepository.GetContractGroupById(contractGroupId);

            if (contractGroup == null)
            {
                return NotFound();
            }
            // Push update to clients
            await _hubContract.Clients.All.SendAsync("ContractGroupUpdated", contractGroupId);

            return Ok(contractGroup);
        }


        [HttpPost]
        [Route(ContractGroupEndpoints.Create)]
        public IActionResult CreateContractGroup([FromBody] ContractGroupCreateModel request)
        {
            _contractGroupRepository.CreateContractGroup(request);
            return Ok();
        }

        [HttpPut]
        [Route(ContractGroupEndpoints.Update)]
        public IActionResult Update(int id, [FromBody] ContractGroupUpdateModel request)
        {
            if (request == null || id != request.Id)
                return BadRequest();

            // Check if the ContractGroup with the specified id exists
            if (!_contractGroupRepository.ContractGroupExit(id))
                return NotFound();

            // Update the ContractGroup and its related data
            _contractGroupRepository.UpdateContractGroup(id, request);

            return Ok();
        }

        [HttpPut]
        [Route(ContractGroupEndpoints.UpdateContractGroupStatus)]
        public IActionResult UpdateContractGroupStatus([FromRoute]int id, [FromBody] ContractGroupUpdateStatusModel request)
        {
            if (request == null || id != request.Id)
                return BadRequest();
            if (!_contractGroupRepository.ContractGroupExit(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if(!_contractGroupRepository.UpdateContractGroupStatus(id, request))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPut]
        [Route(ContractGroupEndpoints.Delete)]
        public IActionResult DeleteContractGroup([FromRoute] int id)
        {
            if (!_contractGroupRepository.ContractGroupExit(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_contractGroupRepository.DeleteContractGroup(id))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


    }

}


  
