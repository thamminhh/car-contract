using CleanArchitecture.Application.Repository;
using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.ExpertiseContract;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class ExpertiseContractController : ControllerBase
    {
        private readonly IExpertiseContractRepository _expertiseContractRepository;
        private readonly FileRepository _fileRepository;


        public ExpertiseContractController(IExpertiseContractRepository expertiseContractRepository, FileRepository fileRepository)
        {
            _expertiseContractRepository = expertiseContractRepository;
            _fileRepository = fileRepository;
        }

        [HttpGet]
        [Route(ExpertiseContractEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpertiseContract>))]
        public IActionResult GetExpertiseContractById(int id)
        {
            if (!_expertiseContractRepository.ExpertiseContractExit(id))
                return NotFound();
            var expertiseContract = _expertiseContractRepository.GetExpertiseContractById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(expertiseContract);
        }

        [HttpGet]
        [Route(ExpertiseContractEndpoints.GetByContractGroupId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpertiseContract>))]
        public IActionResult GetExpertiseContractByContractGroupId(int contractGroupId)
        {
            var expertiseContract = _expertiseContractRepository.GetExpertiseContractByContractGroupId(contractGroupId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(expertiseContract);
        }

        [HttpPost]
        [Route(ExpertiseContractEndpoints.Create)]
        public IActionResult CreateExpertiseContract([FromBody] ExpertiseContractCreateModel request)
        {
            _expertiseContractRepository.CreateExpertiesContract(request);
            return Ok();
        }

        [HttpPut]
        [Route(ExpertiseContractEndpoints.Update)]
        public IActionResult Update(int id, [FromBody] ExpertiseContractUpdateModel request)
        {
            if (request == null || id != request.Id)
                return BadRequest();

            // Check if the car with the specified id exists
            if (!_expertiseContractRepository.ExpertiseContractExit(id))
                return NotFound();

            // Update the car and its related data
            _expertiseContractRepository.UpdateExpertiseContract(id, request);

            return Ok();
        }

        [HttpPut]
        [Route(ExpertiseContractEndpoints.UpdateContractStatus)]
        public IActionResult UpdateCarStatus([FromRoute] int id, [FromBody] ExpertiseContractUpdateStatusModel request)
        {
            if (request == null || id != request.Id)
                return BadRequest();
            if (!_expertiseContractRepository.ExpertiseContractExit(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_expertiseContractRepository.UpdateExpertiseContractStatus(id, request))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpGet("generatePDF")]
        public async Task<IActionResult> GeneratePDF(string contractNo)
        {
            string htmlContent = "<h1> Hợp đồng thuê </h1>";
            string fileName = "Contract_" + contractNo + ".pdf";

            var file = await _fileRepository.GeneratePdfAsync(htmlContent, fileName);

            var filePath = _fileRepository.SaveFileToFolder(file, "1");

            return Ok(filePath);
        }

    }
}


  
