using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebNursePlanning.Models;
using WebNursePlanning.Services.Interfaces;

namespace WebNursePlanning.Views.Appointments.Components
{
	public class ListHealthCareSecondaryViewComponent : ViewComponent
	{
		private readonly IAppointmentsService _appointmentsService;
		public ListHealthCareSecondaryViewComponent(IAppointmentsService appointmentsService)
		{
			_appointmentsService = appointmentsService;
		}
		public async Task<IViewComponentResult> InvokeAsync(int id)
		{
			var healthCareSecondariesList = new HealthCareSecondaryViewModel();
			healthCareSecondariesList.HealthCareSecondaries = await _appointmentsService.GetSelectListHealthCareSecondaryAsync(id);
			
			return View(healthCareSecondariesList);
		}
		
	}
}
