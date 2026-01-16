using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private IConfiguration _configuration;
        public UploadController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            //Read the configuration
            var cnstring = _configuration["BlobStorage:ConnectionString"];
            var containername = _configuration["BlobStorage:ContainerName"];

            //Connect to Container
            var containerClient = new BlobContainerClient(cnstring, containername);
            //Create blob file
            var blobclient = containerClient.GetBlobClient(file.FileName);
            //Upload file
            using var stream = file.OpenReadStream();
            await blobclient.UploadAsync(stream, overwrite: true);
            return Ok("file uploaded successfully..");

        }
    }
}
