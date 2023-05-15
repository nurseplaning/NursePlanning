using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace WebNursePlanning.Services.Interfaces
{
    public interface IHealthCareSecondaryService
    {
        public Task<SelectList> GetSelectListHealthCareSecondaryAsync(int id = 1);
    }
}
