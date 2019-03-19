using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using Appointments.Business.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AppointmentFunctions
{
    public static class NewAppointmentHandler
    {
        [FunctionName("NewAppointment")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequest req,
            [Queue("new-appointments")] IAsyncCollector<NewAppointmentRequest> newAppointmentsQueue,
            ILogger log)
        {
            var secretValue = Environment.GetEnvironmentVariable("TestSecret");
            log.LogInformation($"Secret value is: {secretValue}");

            log.LogInformation($"{nameof(NewAppointmentHandler)} called");

            var content = await new StreamReader(req.Body).ReadToEndAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(content))
            {
                return new BadRequestResult();
            }
            try
            {
                var newAppointmentRequest = JsonConvert.DeserializeObject<NewAppointmentRequest>(content);
                var isValid = newAppointmentRequest != null && newAppointmentRequest.IsValid();

                if (isValid)
                {
                    await newAppointmentsQueue.AddAsync(newAppointmentRequest).ConfigureAwait(false);

                    log.LogInformation($"New appointmentment created for {newAppointmentRequest.Name} :: {newAppointmentRequest.Email} :: {newAppointmentRequest.Number}");
                    return new OkResult();
                }

                return new BadRequestObjectResult(newAppointmentRequest);
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Cannot add a new appointment");
                return new InternalServerErrorResult();
            }
        }
    }
}