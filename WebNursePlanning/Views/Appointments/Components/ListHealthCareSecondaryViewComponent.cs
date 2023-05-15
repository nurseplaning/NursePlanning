using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebNursePlanning.Models;
using WebNursePlanning.Services.Interfaces;

namespace WebNursePlanning.Views.Appointments.Components
{
	public class ListHealthCareSecondaryViewComponent : ViewComponent
	{
		private readonly IHealthCareSecondaryService _healthCareSecondaryService;
		public ListHealthCareSecondaryViewComponent(IHealthCareSecondaryService healthCareSecondaryService)
		{
            _healthCareSecondaryService = healthCareSecondaryService;
		}
		public async Task<IViewComponentResult> InvokeAsync(int id = 1)
		{
			var healthCareSecondariesList = new HealthCareSecondaryViewModel();
			healthCareSecondariesList.HealthCareSecondaries = await _healthCareSecondaryService.GetSelectListHealthCareSecondaryAsync(id);
			
			return View(healthCareSecondariesList);
		}
		
	}
}
