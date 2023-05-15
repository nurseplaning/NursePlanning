using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using WebNursePlanning.Services.Interfaces;

namespace WebNursePlanning.Services
{
    public class HealthCareSecondaryService :IHealthCareSecondaryService
    {
        private readonly IHealthCareSecondaryRepository _healthCareSecondaryRepository;

        public HealthCareSecondaryService(IHealthCareSecondaryRepository healthCareSecondaryRepository)
        {
            _healthCareSecondaryRepository = healthCareSecondaryRepository;
        }
        public async Task<SelectList> GetSelectListHealthCareSecondaryAsync(int id = 1)
        {
            var listHealthCareSecondaries = await _healthCareSecondaryRepository.GetHealthCareSecondaryList(id);
            var dicoHealthCareSecondaries = listHealthCareSecondaries.ToDictionary(b => b.Id, b => b.Name);
            return new SelectList(dicoHealthCareSecondaries, "Key", "Value", id);
        }
    }
}
