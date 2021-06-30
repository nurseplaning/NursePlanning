using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebNursePlanning.Views.Appointments.Components.ModifyStatus
{
    public class ModifyStatus : ViewComponent
    {

        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IStatusRepository _statusRepository;
        

        public ModifyStatus(IAppointmentRepository appointmentRepository, IStatusRepository statusRepository)
        {
            _appointmentRepository = appointmentRepository;
            _statusRepository = statusRepository;
            
        }
      
        public async Task<IViewComponentResult> InvokeAsync(Guid? Id)
        {

            var app = await _appointmentRepository.Details(Id);
            var statusId = await _statusRepository.GetStatusId("Validé");
            app.StatusId = statusId;
            await _appointmentRepository.Edit(app);
            return View(app);

        }
        //...
    }
   
}
