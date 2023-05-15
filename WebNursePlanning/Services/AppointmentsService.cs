using DomainModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNursePlanning.Services.Interfaces;

namespace WebNursePlanning.Services
{
	public class AppointmentsService : IAppointmentsService
	{
		private readonly INurseRepository _nurseRepository;
		private readonly IPatientRepository _patientRepository;
		private readonly IAppointmentRepository _appointmentRepository;

		public AppointmentsService(INurseRepository nurseRepository, IPatientRepository patientRepository, IAppointmentRepository appointmentRepository)
		{
			_nurseRepository = nurseRepository;
			_patientRepository = patientRepository;
			_appointmentRepository = appointmentRepository;
		}

		public async Task<SelectList> GetSelectListNursesAsync(string id = null)
		{
			var listNurses = await _nurseRepository.ListNurses();
			var dicoNurses = listNurses.ToDictionary(b => b.Id, b => b.LastName + " " + b.FirstName);
			return new SelectList(dicoNurses, "Key", "Value", id);
		}

		public async Task<SelectList> GetSelectListPatientsAsync(string id = null)
		{
			var listPatients = await _patientRepository.ListPatients();
			var dicoPatients = listPatients.ToDictionary(b => b.Id, b => b.LastName + " " + b.FirstName);
			return new SelectList(dicoPatients, "Key", "Value", id);
		}

		public async Task<SelectList> GetSelectListHealthCarePrimaryAsync()
		{
			var listHealthCarePrimaries = await _appointmentRepository.GetHealthCarePrimaryList();
			var dicoHealthCarePrimaries = listHealthCarePrimaries.ToDictionary(b => b.Id, b => b.Name);
			return new SelectList(dicoHealthCarePrimaries, "Key", "Value");
		}

		
	}
}