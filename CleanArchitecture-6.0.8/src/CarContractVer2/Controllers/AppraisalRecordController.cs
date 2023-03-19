using CleanArchitecture.Application.Repository;
using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.AppraisalRecord;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]

    public class AppraisalRecordController : ControllerBase
    {
        private readonly IAppraisalRecordRepository _appraisalRecordRepository;
        private readonly FileRepository _fileRepository;


        public AppraisalRecordController(IAppraisalRecordRepository appraisalRecordRepository, FileRepository fileRepository)
        {
            _appraisalRecordRepository = appraisalRecordRepository;
            _fileRepository = fileRepository;
        }

        [HttpGet]
        [Route(AppraisalRecordEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AppraisalRecord>))]
        public IActionResult GetAppraisalRecordById(int id)
        {
            if (!_appraisalRecordRepository.AppraisalRecordExit(id))
                return NotFound();
            var appraisalRecord = _appraisalRecordRepository.GetAppraisalRecordById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(appraisalRecord);
        }

        [HttpGet]
        [Route(AppraisalRecordEndpoints.GetLastByContractGroupId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AppraisalRecord>))]
        public IActionResult GetLastAppraisalByContractGroupId(int contractGroupId)
        {
            var appraisalRecord = _appraisalRecordRepository.GetLastAppraisalRecordByContractGroupId(contractGroupId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(appraisalRecord);
        }

        [HttpGet]
        [Route(AppraisalRecordEndpoints.GetByContractGroupId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AppraisalRecord>))]
        public IActionResult GetAppraisalRecordByContractGroupId(int contractGroupId)
        {
            var appraisalRecord = _appraisalRecordRepository.GetAppraisalRecordByContractGroupId(contractGroupId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(appraisalRecord);
        }

        [HttpPost]
        [Route(AppraisalRecordEndpoints.Create)]
        public IActionResult CreateAppraisalRecord([FromBody] AppraisalRecordCreateModel request)
        {
            if (request == null)
                return BadRequest(ModelState);

            _appraisalRecordRepository.CreateAppraisalRecord(request);
            return Ok();
        }

        [HttpPut]
        [Route(AppraisalRecordEndpoints.Update)]
        public IActionResult Update(int id, [FromBody] AppraisalRecordUpdateModel request)
        {
            if (request == null || id != request.Id)
                return BadRequest();

            // Check if the car with the specified id exists
            if (!_appraisalRecordRepository.AppraisalRecordExit(id))
                return NotFound();

            // Update the car and its related data
            _appraisalRecordRepository.UpdateAppraisalRecord(id, request);

            return Ok();
        }

    }
}



