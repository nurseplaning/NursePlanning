using DomainModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebNursePlanning.Services
{
	public class AppointmentsService
	{
		private readonly INurseRepository _nurseRepository;
		private readonly IPatientRepository _patientRepository;

		public AppointmentsService(INurseRepository nurseRepository, IPatientRepository patientRepository)
		{
			_nurseRepository = nurseRepository;
			_patientRepository = patientRepository;
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
	}
}