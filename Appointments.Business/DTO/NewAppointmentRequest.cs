namespace Appointments.Business.DTO
{
    public class NewAppointmentRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }

        public bool IsValid() => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Number);
    }
}