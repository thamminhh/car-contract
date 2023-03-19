
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.Content.Objects;
using PdfSharpCore.Utils;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace CleanArchitecture.Application.Repository;
public class FileRepository

{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public FileRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
    {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
    }

    public PdfDocument GeneratePdfAsync(string htmlContent, string fileName)
    {
        var document = new PdfDocument();
        PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);
        return document;
    }

    public string SaveFileToFolder(PdfDocument pdfDocument, string subFolderName)
    {
        string folderName = "pdfs";
        string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName, subFolderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string uniqueFilename = Guid.NewGuid().ToString() + ".pdf";
        string serverFilePath = Path.Combine(folderPath, uniqueFilename);

        pdfDocument.Save(serverFilePath);

        return "/" + folderName + "/" + subFolderName + "/" + uniqueFilename;
    }


    //    public string SaveImageToFolder(IFormFile? file, string subFolderName)
    //    {

    //        string folderName = "cars";
    //        string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName, subFolderName);
    //        if (!Directory.Exists(folderPath))
    //        {
    //            Directory.CreateDirectory(folderPath);
    //        }

    //        string uniqueFilename = Guid.NewGuid().ToString() + "_" + file.FileName;
    //        string serverFilePath = Path.Combine(folderPath, uniqueFilename);

    //        using (var fileStream = new FileStream(serverFilePath, FileMode.Create))
    //        {
    //            file.CopyToAsync(fileStream);
    //        }

    //        return "/" + folderName + "/" + subFolderName + "/" + uniqueFilename;
    //    }

    //    public string SaveFileToFolder(IFormFile file, string subFolderName)
    //    {
    //            if (file == null || file.Length == 0)
    //            {
    //                throw new ArgumentException("File is empty.");
    //            }
    //            string folderName = "contracts";
    //            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName, subFolderName);
    //            if (!Directory.Exists(folderPath))
    //            {
    //                Directory.CreateDirectory(folderPath);
    //            }

    //            string uniqueFilename = Guid.NewGuid().ToString() + "_" + file.FileName;
    //            string serverFilePath = Path.Combine(folderPath, uniqueFilename);

    //            using (var fileStream = new FileStream(serverFilePath, FileMode.Create))
    //            {
    //                file.CopyToAsync(fileStream);
    //            }

    //            return "/" + folderName + "/" + subFolderName + "/" + uniqueFilename;
    //        }


    //public IFormFile GeneratePdfAsync(string htmlContent, string fileName)
    //{
    //    var document = new PdfDocument();
    //    PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);
    //    byte[] response = null;
    //    using (MemoryStream ms = new MemoryStream())
    //    {
    //        document.Save(ms);

    //        response = ms.ToArray();
    //    }

    //    var file = new FormFile(new MemoryStream(response), 0, response.Length, fileName, fileName);

    //    return file;
    //}

    public string GetCurrentHost()
        {
                var request = _httpContextAccessor.HttpContext?.Request;
                var host = request?.Host.Value;

                return "https://" + host;
        }

    //public IFormFile GeneratePdfAsync(string htmlContent, string fileName)
    //{
    //    // Create a new HtmlToPdf object
    //    HtmlToPdf htmlToPdf = new HtmlToPdf();

    //    // Convert the HTML content to a PDF document
    //    IronPdf.PdfDocument pdfDocument = htmlToPdf.RenderHtmlAsPdf(htmlContent);

    //    // Convert the PDF document to a byte array
    //    byte[] pdfBytes = pdfDocument.BinaryData;

    //    // Create a new MemoryStream object to hold the PDF bytes
    //    MemoryStream memoryStream = new MemoryStream(pdfBytes);

    //    // Create a new FormFile object from the MemoryStream
    //    IFormFile formFile = new FormFile(memoryStream, 0, pdfBytes.Length, fileName, fileName);

    //    // Dispose the PDF document
    //    pdfDocument.Dispose();

    //    // Return the FormFile object
    //    return formFile;
    //}
    //public void GeneratePDF(string htmlContent, string fileName)
    //{
    //    // Create a new HtmlToPdf object
    //    HtmlToPdf htmlToPdf = new HtmlToPdf();

    //    // Convert the HTML content to a PDF document
    //    IronPdf.PdfDocument pdfDocument = htmlToPdf.RenderHtmlAsPdf(htmlContent);

    //    // Save the PDF document to disk with the specified file name
    //    pdfDocument.SaveAs(fileName);

    //    // Dispose the PDF document
    //    pdfDocument.Dispose();
    //}

    //public void GeneratePDF(string htmlContent, string fileName)
    //{
    //    // Create a new HtmlToPdf object
    //    HtmlToPdf htmlToPdf = new HtmlToPdf();

    //    // Convert the HTML content to a PDF document
    //    PdfDocument pdfDocument = htmlToPdf.RenderHtmlAsPdf(htmlContent);

    //    // Save the PDF document to disk with the specified file name
    //    pdfDocument.SaveAs(fileName);

    //    // Dispose the PDF document
    //    pdfDocument.Dispose();
    //}

    //public IFormFile GeneratePdfAsync(string htmlContent, string fileName)
    //{
    //    var document = new PdfDocument();

    //    try
    //    {
    //        PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);
    //        byte[] response = null;
    //        using (MemoryStream ms = new MemoryStream())
    //        {
    //            document.Save(ms);
    //            response = ms.ToArray();
    //        }

    //        var file = new FormFile(new MemoryStream(response), 0, response.Length, fileName, fileName);
    //        return file;
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the error message or details
    //        Console.WriteLine("PDF file creation failed: " + ex.Message);
    //        return null;
    //    }
    //    finally
    //    {
    //        // Make sure to dispose the PDF document
    //        document.Dispose();
    //    }
    //}

}
