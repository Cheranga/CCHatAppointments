using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AppointmentFunctions
{
    public static class Scheduler
    {
        [FunctionName("Scheduler")]
        public static void Run([QueueTrigger("new-appointments", Connection = "")]
            string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}