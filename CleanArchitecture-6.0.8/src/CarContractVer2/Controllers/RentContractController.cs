using System.Drawing.Imaging;
using System.Drawing;
using CleanArchitecture.Application.Repository;
using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.RentContract;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf.IO;
using CleanArchitecture.Domain.Entities_SubModel.RentContractFile.Sub_Model;
using NuGet.Protocol.Core.Types;

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class RentContractController : ControllerBase
    {
        private readonly IRentContractRepository _rentContractRepository;
        private readonly IRentContractFileRepository _rentContractFileRepository;


        public RentContractController(IRentContractRepository rentContractRepository, IRentContractFileRepository rentContractFileRepository)
        {
            _rentContractRepository = rentContractRepository;
            _rentContractFileRepository = rentContractFileRepository;
        }

        [HttpGet]
        [Route(RentContractEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RentContract>))]
        public IActionResult GetRentContractById(int id)
        {
            if (!_rentContractRepository.RentContractExit(id))
                return NotFound();
            var rentContract = _rentContractRepository.GetRentContractById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rentContract);
        }

        [HttpGet]
        [Route(RentContractEndpoints.GetLastByContractGroupId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RentContract>))]
        public IActionResult GetLastRentContractByContractGroupId(int contractGroupId)
        {
            var rentContract = _rentContractRepository.GetLastRentContractByContractGroupId(contractGroupId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rentContract);
        }

        [HttpGet]
        [Route(RentContractEndpoints.GetByContractGroupId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RentContract>))]
        public IActionResult GetListRentContractByContractGroupId(int contractGroupId)
        {
            var rentContract = _rentContractRepository.GetRentContractByContractGroupId(contractGroupId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rentContract);
        }

        [HttpGet]
        [Route(RentContractEndpoints.GetRentContractFilesByContractId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RentContract>))]
        public IActionResult GetRentContractFilesByContractId(int rentContractId)
        {
            var rentContractFiles = _rentContractFileRepository.GetRentContractFilesByRentContractId(rentContractId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rentContractFiles);
        }

        [HttpPost]
        [Route(RentContractEndpoints.Create)]
        public IActionResult CreaterentContract([FromBody] RentContractCreateModel request)
        {
            _rentContractRepository.CreateRentContract(request);
            return Ok();
        }

        [HttpPost]
        [Route(RentContractEndpoints.CreateRentContractFile)]
        public async Task<IActionResult> CreateRentContractFiles(List<RentContractFileCreateModel> rentContractFiles)
        {
            await _rentContractFileRepository.CreateRentContractFiles(rentContractFiles);

            return Ok();
        }

        [HttpPut]
        [Route(RentContractEndpoints.Update)]
        public IActionResult Update(int id, [FromBody] RentContractUpdateModel request)
        {
            if (request == null || id != request.Id)
                return BadRequest();

            // Check if the car with the specified id exists
            if (!_rentContractRepository.RentContractExit(id))
                return NotFound();

            // Update the car and its related data
            _rentContractRepository.UpdateRentContract(id, request);

            return Ok();
        }

        [HttpPut]
        [Route(RentContractEndpoints.UpdateContractStatus)]
        public IActionResult UpdateContractStatus([FromRoute] int id, [FromBody] RentContractUpdateStatusModel request)
        {
            if (request == null || id != request.Id)
                return BadRequest();
            if (!_rentContractRepository.RentContractExit(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_rentContractRepository.UpdateRentContractStatus(id, request))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPut]
        [Route(RentContractEndpoints.UpdateRentContractFile)]
        public async Task<IActionResult> UpdateRentContractFiles(List<RentContractFileUpdateModel> rentContractFiles)
        {
            await _rentContractFileRepository.UpdateRentContractFiles(rentContractFiles);

            return Ok();
        }

        [HttpDelete]
        [Route(RentContractEndpoints.DeleteRentContractFile)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(int rentContractFileId)
        {
            bool deleted = await _rentContractFileRepository.DeleteRentContractFile(rentContractFileId);

            if (deleted)
            {
                return Ok("Deleted"); // Object deleted successfully
            }
            return NotFound(); // Object not found
        }
        //[HttpPut]
        //[Route(RentContractEndpoints.UpdateContractSigned)]
        //public IActionResult UpdateContractSigned([FromRoute] int id, [FromBody] RentContractUpdateSignedModel request)
        //{
        //    if (request == null || id != request.Id)
        //        return BadRequest();
        //    if (!_rentContractRepository.RentContractExit(id))
        //        return NotFound();
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    if (!_rentContractRepository.UpdateRentContractSigned(id, request))
        //    {
        //        ModelState.AddModelError("", "Something went wrong");
        //        return StatusCode(500, ModelState);
        //    }
        //    return NoContent();
        //}

        //[HttpPost("addimage")]
        //public async Task<IActionResult> AddImageToPdf(string imageUrl, IFormFile pdfFile)
        //{
        //    // Load the PDF file
        //    using var pdfStream = pdfFile.OpenReadStream();
        //    using var pdfDocument = PdfReader.Open(pdfStream, PdfDocumentOpenMode.Modify);

        //    // Load the image from the URL
        //    using var httpClient = new HttpClient();
        //    using var imageStream = await httpClient.GetStreamAsync(imageUrl);
        //    var image = XImage.FromStream(() => imageStream);

        //    // Add the image to the last page of the PDF document
        //    var page = pdfDocument.Pages[pdfDocument.PageCount - 1];
        //    var gfx = XGraphics.FromPdfPage(page);
        //    gfx.DrawImage(image, new XRect(100, 100, 200, 200));

        //    // Save the modified PDF document to a MemoryStream
        //    using var outputStream = new MemoryStream();
        //    pdfDocument.Save(outputStream);
        //    outputStream.Position = 0;

        //    // Return the modified PDF file as a FileStreamResult
        //    var result = new FileStreamResult(outputStream, "application/pdf");
        //    result.FileDownloadName = "output.pdf";
        //    return result;
        //}


        //[HttpPost("addimage")]
        //public async Task<IActionResult> AddImageToPdf(IFormFile imageFile, IFormFile pdfFile)
        //{
        //    // Load the PDF file
        //    var pdfStream = pdfFile.OpenReadStream();
        //    var pdfMemoryStream = new MemoryStream();
        //    await pdfStream.CopyToAsync(pdfMemoryStream);
        //    pdfMemoryStream.Position = 0;
        //    var pdfDocument = PdfReader.Open(pdfMemoryStream, PdfDocumentOpenMode.Modify);

        //    // Load the image from the IFormFile object
        //    var imageStream = imageFile.OpenReadStream();
        //    var imageMemoryStream = new MemoryStream();
        //    var buffer = new byte[131072];
        //    var bufferedStream = new BufferedStream(imageStream);
        //    int bytesRead;
        //    while ((bytesRead = await bufferedStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        //    {
        //        await imageMemoryStream.WriteAsync(buffer, 0, bytesRead);
        //    }
        //    bufferedStream.Dispose();
        //    imageMemoryStream.Position = 0;
        //    var image = XImage.FromStream(() => imageMemoryStream);

        //    // Calculate the size and position of the image on the PDF page
        //    var page = pdfDocument.Pages[pdfDocument.PageCount - 1];
        //    var pageWidth = page.Width.Point;
        //    var pageHeight = page.Height.Point;
        //    var imageSize = Math.Min(pageWidth, pageHeight) / 4;
        //    var imageX = 0;
        //    var imageY = pageHeight - imageSize;

        //    // Add the image to the last page of the PDF document
        //    var gfx = XGraphics.FromPdfPage(page);
        //    gfx.DrawImage(image, new XRect(imageX, imageY, imageSize, imageSize));

        //    // Save the modified PDF document to a MemoryStream
        //    var outputStream = new MemoryStream();
        //    pdfDocument.Save(outputStream);
        //    outputStream.Position = 0;

        //    // Return the modified PDF file as a FileStreamResult
        //    var result = new FileStreamResult(outputStream, "application/pdf");
        //    result.FileDownloadName = "output.pdf";
        //    return result;
        //}

    }
}






