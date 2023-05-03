using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace WebNursePlanning.Services.Interfaces
{
	public interface IAppointmentsService
	{
		public Task<SelectList> GetSelectListNursesAsync(string id = null);

		public Task<SelectList> GetSelectListPatientsAsync(string id = null);

		public Task<SelectList> GetSelectListHealthCarePrimaryAsync();

		public Task<SelectList> GetSelectListHealthCareSecondaryAsync(int id = 1);
	}
}
