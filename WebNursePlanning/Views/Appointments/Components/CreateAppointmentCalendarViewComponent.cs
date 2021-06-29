using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebNursePlanning.Shared.Components
{
    public class CreateAppointmentCalendarViewComponent : ViewComponent
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public CreateAppointmentCalendarViewComponent(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id, int decalage =0)
        {

            Dictionary<string, List<TimeSpan>> dico = await _appointmentRepository.GetListAvailableAppointments(id,decalage:decalage);

            ViewData["AppointmentsAvailables"] = dico;
            return View();
        }
       
    }
}
