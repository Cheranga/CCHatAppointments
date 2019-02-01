using Appointments.Business;
using Appointments.Business.Models;
using Autofac;
using AzureFunctions.Autofac.Configuration;

namespace AppointmentFunctions
{
    public class IoCConfig
    {
        public IoCConfig(string functionName)
        {
            DependencyInjection.Initialize(builder => { builder.RegisterType<AppointmentDecider>().As<IAppointmentDecider<NextAvailableAppointment>>(); }
                , functionName);
        }
    }
}