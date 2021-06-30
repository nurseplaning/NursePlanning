using DomainModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAppointmentRepository
    {
        public Task<IEnumerable<Appointment>> ListAppointments();
        public Task<List<Appointment>> ListAppointmentsById(string id);
        public Task<Appointment> Details(Guid? id);
        public Task<Appointment> Create(Appointment appointment);
        public Task Edit(Appointment appointment);
        public Task Transfer(Appointment appointment);
        public Task Delete(Guid? id);
        public bool Exists(Guid? id);
        public Task<Dictionary<string, List<TimeSpan>>> GetListAvailableAppointments(string personId, List<Appointment> appToEdit = null, int decalage = 0);
        public bool CheckAvailabilityAppointment(List<Appointment> appointments, DateTime appointmentDate);
        public bool IsPast(DateTime appointmentDate, TimeSpan appointmentTime);
        public DateTime GetFirstDayOfWeek(DateTime dayInWeek);
        public DateTime GetFirstDateOfWeek(DateTime dayInWeek, CultureInfo cultureInfo);
    }
}