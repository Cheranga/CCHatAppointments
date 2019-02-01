using Appointments.Business.Models;

namespace Appointments.Business
{
    public class AppointmentDecider : IAppointmentDecider<NextAvailableAppointment>
    {
        public bool IsAvailable(NextAvailableAppointment appointment)
        {
            //
            // Note that for demo purposes, minimum validation is performed
            //
            if (appointment == null)
            {
                return false;
            }

            //
            // Note: The appointment will be selected on the second of "from" and "to". If both of them are even the appointment can be made otherwise no
            //
            var secondInFrom = appointment.From.Second;
            var secondInTo = appointment.To.Second;

            var canTheAppointmentBeMade = secondInFrom % 2 == 0 && secondInTo % 2 == 0;

            return canTheAppointmentBeMade;
        }
    }
}