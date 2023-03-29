using DomainModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebNursePlanning.Services.Interfaces;

namespace WebNursePlanning.Models
{
	public class HealthCareSecondaryViewModel
	{

		public HealthCareSecondaryViewModel()
		{
		}
		[Display(Name = "Type de soin")]
		public int HealthCareSecondaryId { get; set; }
		public SelectList HealthCareSecondaries { get; set; }
	}
}
