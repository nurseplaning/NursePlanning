using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebNursePlanning.Shared.Components
{
    public class EditAppointmentCalendarViewComponent : ViewComponent
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public EditAppointmentCalendarViewComponent(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(string personId, List<Appointment> appToEdit)
        {

            Dictionary<string, List<TimeSpan>> dico = await _appointmentRepository.GetListAvailableAppointments(personId, appToEdit);

            ViewData["AppointmentsAvailables"] = dico;
            return View();
        }
       
    }
}
