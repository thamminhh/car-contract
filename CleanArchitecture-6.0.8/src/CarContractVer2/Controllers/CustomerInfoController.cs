using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CustomerInfos;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]

    public class CustomerInfoController : ControllerBase
    {
        private readonly ICustomerInfoRepository _customerInfoController;


        public CustomerInfoController(ICustomerInfoRepository customerInfoController)
        {
            _customerInfoController = customerInfoController;
        }

        [HttpGet]
        [Route(CustomerInfoEndpoints.GetAll)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CustomerInfo>))]
        public IActionResult GetCustomerInfos()
        {
            var listCustomerInfo = _customerInfoController.GetCustomerInfos();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(listCustomerInfo);
        }

        //[HttpGet]
        //[Route(CustomerInfoEndpoints.GetSingle)]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<CustomerInfo>))]
        //public IActionResult GetCustomerInfoById(int id)
        //{
        //    if (!_customerInfoController.CustomerInfoExit(id))
        //        return NotFound();
        //    var customerInfo = _customerInfoController.GetCustomerInfoById(id);
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    return Ok(customerInfo);
        //}

        [HttpGet]
        [Route(CustomerInfoEndpoints.GetByCitizenIdentificationInfoNumber)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CustomerInfo>))]
        public IActionResult GetCustomerInfoByCitizenIdentificationInfoNumber(string citizenIdentificationInfoNumber)
        {
            if (!_customerInfoController.CustomerInfoExit(citizenIdentificationInfoNumber))
                    return NotFound();
                var customerInfo = _customerInfoController.GetCustomerInfoByCitizenIdentificationInfoNumber(citizenIdentificationInfoNumber);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(customerInfo);
        }

        [HttpPost]
        [Route(CustomerInfoEndpoints.Create)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCustomerInfo([FromBody] CustomerInfo customerInfoCreate)
        {
            if (customerInfoCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_customerInfoController.CreateCustomerInfo(customerInfoCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Added");
        }

        [HttpPut]
        [Route(CustomerInfoEndpoints.UpdateByCitizenIdentificationInfoNumber)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCustomerInfo(string citizenIdentificationInfoNumber, [FromBody] CustomerInfoUpdateModel customerInfoUpdate)
        {
            if (customerInfoUpdate == null)            
                return BadRequest(ModelState);
            
            if(citizenIdentificationInfoNumber != customerInfoUpdate.CitizenIdentificationInfoNumber)
                return BadRequest(ModelState);

            if (!_customerInfoController.CustomerInfoExit(citizenIdentificationInfoNumber))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_customerInfoController.UpdateCustomerInfo(citizenIdentificationInfoNumber, customerInfoUpdate))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}


  
