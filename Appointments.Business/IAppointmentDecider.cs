namespace Appointments.Business
{
    public interface IAppointmentDecider<in TAppointment> where TAppointment:class 
    {
        bool IsAvailable(TAppointment appointment);
    }
}