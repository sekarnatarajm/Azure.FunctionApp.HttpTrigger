using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using MyHttpFunction.Models;
using System.Text;
using System.Text.Json;

namespace MyHttpFunction
{
    public class TimerTriggerFunction
    {
        private readonly ILogger<TimerTriggerFunction> _logger;
        public TimerTriggerFunction(ILogger<TimerTriggerFunction> logger)
        {
            _logger = logger;
        }
        [FunctionName("TimerTriggerFun")]
        public void Run([TimerTrigger("0 */2 * * * *",

#if DEBUG
    RunOnStartup= true
#endif

            )] TimerInfo timerInfo, ILogger log)
        {
            try
            {
                //log.LogInformation($"Function started at: {DateTime.Now}");
                //string fileName = "";
                //string blobName = fileName;
                //string containerName = "TimerSchedule";
                //string blboConnection = "DefaultEndpointsProtocol=https;AccountName=azurestoragelearningdemo;AccountKey=AYAc32iMtJYXjEXXybWfXiGlwwpiBF9PV1/cPjMa/bs61XZM3nOlGOgjXeHb5I5gcDhkuYih1jKB+AStO2QY9A==;EndpointSuffix=core.windows.net";

                //log.LogInformation($"Function Ran. Next timer schedule = {timerInfo.ScheduleStatus?.Next}");
                //string localPath = Path.Combine(Environment.CurrentDirectory, fileName);
                //var jsonString = JsonSerializer.Serialize(GetPersonDatas(), new JsonSerializerOptions { WriteIndented = true });
                //File.WriteAllText(localPath, jsonString);

                ////Blob
                //BlobServiceClient blobServiceClient = new BlobServiceClient(blboConnection);
                //BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
                //await blobContainerClient.CreateIfNotExistsAsync();
                //BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);

                //using FileStream fileStream = File.OpenRead(localPath);
                //blobClient.Upload(fileStream);
                //fileStream.Close();
                //log.LogInformation($"Task completed successfully. Result");

                string logMessage = $"Function executed at: {DateTime.UtcNow}\n";

                if (timerInfo.IsPastDue)
                {
                    logMessage += "Warning: Timer is running late!\n";
                }

                logMessage += "Performing scheduled task...\n";
                int result = 42; // Simulated result
                logMessage += $"Task completed. Result: {result}\n";

                SaveLogToBlob(logMessage);
            }
            catch (Exception ex)
            {
                log.LogError($"An error occurred: {ex.Message}");
                throw;
            }
        }
        public static List<EmployeeSkill> GetPersonDatas()
        {
            return new List<EmployeeSkill>()
            {
                new EmployeeSkill{
                Name = "Sekar Nataraj",
                Age = 30,
                Skills = new List<string> { "C#", "Azure", "SQL" }
            }
            };
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
