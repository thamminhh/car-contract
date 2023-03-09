using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PdfSharpCore;
using PdfSharpCore.Pdf;
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
        public string SaveImageToFolder(IFormFile? file, string subFolderName)
        {
            
            string folderName = "cars";
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName, subFolderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string uniqueFilename = Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFilePath = Path.Combine(folderPath, uniqueFilename);

            using (var fileStream = new FileStream(serverFilePath, FileMode.Create))
            {
                file.CopyToAsync(fileStream);
            }

            return "/" + folderName + "/" + subFolderName + "/" + uniqueFilename;
        }

        public string SaveFileToFolder(IFormFile file, string subFolderName)
        {
                if (file == null || file.Length == 0)
                {
                    throw new ArgumentException("File is empty.");
                }
                string folderName = "contracts";
                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName, subFolderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string uniqueFilename = Guid.NewGuid().ToString() + "_" + file.FileName;
                string serverFilePath = Path.Combine(folderPath, uniqueFilename);

                using (var fileStream = new FileStream(serverFilePath, FileMode.Create))
                {
                    file.CopyToAsync(fileStream);
                }

                return "/" + folderName + "/" + subFolderName + "/" + uniqueFilename;
            }

            public IFormFile GeneratePdfAsync(string htmlContent, string fileName)
            {
                var document = new PdfDocument();
                PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);
                byte[] response = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    document.Save(ms);
            
                    response = ms.ToArray();
                }

                var file = new FormFile(new MemoryStream(response), 0, response.Length, fileName, fileName);

                return file;
            }

            public string GetCurrentHost()
            {
                var request = _httpContextAccessor.HttpContext?.Request;
                var host = request?.Host.Value;

                return "https://" + host;
            }


}
