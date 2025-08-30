using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using MyHttpFunction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyHttpFunction
{
    public class NotificationService
    {
        public NotificationService() { }
        [Function("sendemail")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest request)
        {
            var notifyData = await new StreamReader(request.Body).ReadToEndAsync();
            var data = JsonSerializer.Deserialize<NotificationData>(notifyData);
            return new OkObjectResult(true);
        }
    }
}
