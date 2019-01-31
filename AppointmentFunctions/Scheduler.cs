using Appointments.Business;
using Appointments.Business.Models;
using AzureFunctions.Autofac;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AppointmentFunctions
{
    [DependencyInjectionConfig(typeof(IoCConfig))]
    public static class Scheduler
    {
        [FunctionName("Scheduler")]
        public static void Run(
            [QueueTrigger("new-appointments", Connection = "")]
            string myQueueItem,
            [Inject]
            IAppointmentDecider<NextAvailableAppointment> decider,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}