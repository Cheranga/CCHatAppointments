using System;
using Appointments.Business;
using Appointments.Business.DTO;
using Appointments.Business.Models;
using AzureFunctions.Autofac;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AppointmentFunctions
{
    [DependencyInjectionConfig(typeof(IoCConfig))]
    public static class Scheduler
    {
        [FunctionName("Scheduler")]
        public static void Run(
            [QueueTrigger("new-appointments", Connection = "")]NewAppointmentRequest newAppointment,
            [Blob("scheduled-appointments/{rand-guid}")]out string scheduledAppointment,
            [Blob("rejected-requests/{rand-guid}")]out string rejectedAppointment,
            [Inject]IAppointmentDecider<NextAvailableAppointment> decider,
            ILogger log)
        {
            log.LogInformation($"{nameof(Scheduler)} started");

            scheduledAppointment = null;
            rejectedAppointment = null;

            var nextAvailableAppointment = new NextAvailableAppointment
            {
                Email = newAppointment.Email,
                From = DateTime.UtcNow,
                To = DateTime.UtcNow.AddHours(1)
            };

            var isAvailable = decider.IsAvailable(nextAvailableAppointment);
            if (isAvailable)
            {
                log.LogInformation($"Appointment is scheduled for {nextAvailableAppointment.Email}");
                scheduledAppointment = JsonConvert.SerializeObject(nextAvailableAppointment);
            }
            else
            {
                log.LogInformation($"Appointment is rejected for {nextAvailableAppointment.Email}");
                rejectedAppointment = JsonConvert.SerializeObject(newAppointment);
            }
        }
    }
}