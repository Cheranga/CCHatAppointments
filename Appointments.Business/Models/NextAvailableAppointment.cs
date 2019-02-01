using System;

namespace Appointments.Business.Models
{
    public class NextAvailableAppointment
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Email { get; set; }
    }
}