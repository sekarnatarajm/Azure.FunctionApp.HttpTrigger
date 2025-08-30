using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using MyHttpFunction.Models;

namespace MyHttpFunction
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("GetStudents-Old")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            List<Students> students = new List<Students>()
            {
                new Students(){ID = 1,FirstName = "Sekar",LastName = "Nataraj",Gender="Male"},
                new Students(){ID = 2,FirstName = "Krishiv",LastName = "Sekar",Gender="Male"},
                new Students(){ID = 3,FirstName = "Nandhu",LastName = "Sekar",Gender="FeMale"}
            };
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult(students);
        }
    }
}
