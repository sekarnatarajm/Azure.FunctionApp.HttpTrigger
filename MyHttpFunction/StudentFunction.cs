using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MyHttpFunction.Models;
using System.Text;

namespace MyHttpFunction
{
    public class StudentFunction
    {
        private readonly ILogger<StudentFunction> _logger;

        public StudentFunction(ILogger<StudentFunction> logger)
        {
            _logger = logger;
        }
        [Function("GetStudents")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            List<Students> students = new List<Students>()
            {
                new Students(){ID = 1,FirstName = "Sekar",LastName = "Nataraj",Gender="Male"},
                new Students(){ID = 2,FirstName = "Krishiv",LastName = "Sekar",Gender="Male"},
                new Students(){ID = 3,FirstName = "Nandhu",LastName = "Sekar",Gender="FeMale"}
            };
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            SaveLogToBlob("Test");
            return new OkObjectResult(students);
        }

        private void SaveLogToBlob(string logContent)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=azurestoragelearningdemo;AccountKey=AYAc32iMtJYXjEXXybWfXiGlwwpiBF9PV1/cPjMa/bs61XZM3nOlGOgjXeHb5I5gcDhkuYih1jKB+AStO2QY9A==;EndpointSuffix=core.windows.net";
            string containerName = "function-logs";
            string blobName = $"log-{DateTime.UtcNow:yyyy-MM-dd-HH-mm-ss}.txt";

            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            containerClient.CreateIfNotExists();

            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(logContent));
            blobClient.Upload(stream, overwrite: true);
        }
    }
}
