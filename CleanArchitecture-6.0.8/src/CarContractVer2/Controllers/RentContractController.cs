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

namespace CarContractVer2.Controllers
{
    [ApiController]
    
    public class RentContractController : ControllerBase
    {
        private readonly IRentContractRepository _rentContractRepository;


        public RentContractController(IRentContractRepository rentContractRepository)
        {
            _rentContractRepository = rentContractRepository;
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
        [Route(RentContractEndpoints.GetByContractGroupId)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RentContract>))]
        public IActionResult GetRentContractByContractGroupId(int contractGroupId)
        {
            var rentContract = _rentContractRepository.GetRentContractByContractGroupId(contractGroupId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rentContract);
        }


        [HttpPost]
        [Route(RentContractEndpoints.Create)]
        public IActionResult CreaterentContract([FromBody] RentContractCreateModel request)
        {
            _rentContractRepository.CreateRentContract(request);
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
        public IActionResult UpdateCarStatus([FromRoute] int id, [FromBody] RentContractUpdateStatusModel request)
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






