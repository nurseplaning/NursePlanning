using Dal;
using DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IAbsenceRepository _absenceRepository;

        public AppointmentRepository(ApplicationDbContext context, IAbsenceRepository absenceRepository)
        {
            _context = context;
            _absenceRepository = absenceRepository;
        }

        public async Task<IEnumerable<Appointment>> ListAppointments()
        {
            return await _context.Appointments.Include(a => a.Nurse).Include(a => a.Patient).Include(a => a.Status).ToListAsync();
        }

        public async Task<Appointment> Details(Guid? id)
        {
            var appointment = await _context.Appointments
                                    .Include(a => a.Nurse)
                                    .Include(a => a.Patient)
                                    .Include(a => a.Status)
                                    .FirstOrDefaultAsync(m => m.Id == id);
            return appointment;
        }

        public async Task<Appointment> Create(Appointment appointment)
        {

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }

        public async Task Edit(Appointment appointment)
        {
            _context.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task Transfer(Appointment appointment)
        {
            _context.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid? id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointment);

            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid? id)
        {
            return _context.Appointments.Any(a => a.Id == id);
        }
        [Authorize]
        public async Task<List<Appointment>> ListAppointmentsById(string idPerson)
        {
            return await _context.Appointments.Include(a => a.Nurse).Include(a => a.Patient).Include(a => a.Status).Where(p => p.NurseId == idPerson || p.PatientId == idPerson).ToListAsync();
        }

        public async Task<Dictionary<string, List<TimeSpan>>> GetListAvailableAppointments(string personId, List<Appointment> appToEdit = null, int decalage = 0)
        {
            //En mode Edition de rdv on doit garder le rdv (appToEdit) à editer dans le dico renvoyé

            //En mode Create car pas de rdv à editer, appToEdit est vide c'est normal

            //En mode Edit appToEdit existe

            //Define start time and end time for taking appointments
            TimeSpan startTime = new(8, 0, 0);
            TimeSpan endTime = new(17, 30, 0);
            DateTime dateOfWeek = DateTime.Now.Date.AddDays(decalage*7);
            DateTime completeDate= dateOfWeek.Add(startTime);
            TimeSpan delayAppointment = new(0, 30, 0);

            //Get Total minutes of a day
            TimeSpan timeInterval = endTime.Subtract(startTime);
            //Va contenir la liste des heures de rdv sur une journée
            List<TimeSpan> listTimes = new();

            //Calculate how many time slots  of appointments in a day
            double nbAppointments = timeInterval.Divide(delayAppointment);
            //Get existing appointments from database
            List<Appointment> listAppointments = (List<Appointment>)await ListAppointmentsById(personId);
            //Get existing absences from database
            var listAbsences = (List<Absence>)await _absenceRepository.ListAbsenceById(personId);

            if (appToEdit != null)
                if (listAppointments.Contains(appToEdit.FirstOrDefault()))
                    listAppointments.Remove(appToEdit.FirstOrDefault());

            //Create List of Appointments
            Dictionary<string, List<TimeSpan>> dicoAppointments = new();
            for (int day = 0; day < 7; day++)
            {
                
                for (int timeappointment = 0; timeappointment < nbAppointments; timeappointment++)
                {
                    if (CheckAvailabilityAppointment(listAppointments, completeDate) 
                        && CheckAvailabilityAbsences(listAbsences, dateOfWeek, startTime, delayAppointment)
                        && !IsPast(dateOfWeek, startTime))
                        listTimes.Add(startTime);
                    else
                        listTimes.Add(new TimeSpan());
                    
                    startTime = startTime.Add(delayAppointment);
                    completeDate = dateOfWeek.Add(startTime);
                }
                //Ajout dans le dictionnaire des dates de rdvs
                if (!dicoAppointments.ContainsKey(dateOfWeek.ToString("dddd dd MMMM yyyy")))
                    dicoAppointments.Add(dateOfWeek.ToString("dddd dd MMMM yyyy"), listTimes);

                //Preparation des variables pour le jour suivant
                listTimes = new List<TimeSpan>();
                dateOfWeek = dateOfWeek.AddDays(1);
                startTime = new TimeSpan(8, 0, 0);
                completeDate = dateOfWeek.Add(startTime);
            }
            //Une fois les rdvs dispo verifiés, on verifie les absences avec les rdvs dispos

            return dicoAppointments;
        }

        
        public bool CheckAvailabilityAppointment(List<Appointment> appointments, DateTime appointmentDate)
        {
            //Par default, il considere que le rdv est disponible, cas d'une comparaison avec une liste de rdvs vide passée en parametre
            bool isAvailable = true;           
            foreach (var item in appointments)
            {
                if (item.Date.Day == appointmentDate.Day && item.Date.Hour == appointmentDate.Hour && item.Date.Minute == appointmentDate.Minute)
                {
                    isAvailable = false;
                    break;
                }
                else
                    isAvailable = true;
            }
            return isAvailable;
        }

        public bool CheckAvailabilityAbsences(List<Absence> absences, DateTime appointmentDay, TimeSpan appointmentTime, TimeSpan appointmentDuration)
        {
            bool isAvailable = true;
            DateTime appointmentDate = appointmentDay.Add(appointmentTime);
            foreach (var absence in absences)
            {
                if (absence.DateEnd.CompareTo(appointmentDate) == -1 || absence.DateStart.CompareTo(appointmentDate.Add(appointmentDuration)) == 1)
                    isAvailable = true;
                else
                {
                    isAvailable = false;
                    break;
                }
            }

            return isAvailable;
        }

        public bool IsPast(DateTime appointmentDate, TimeSpan appointmentTime)
        {
            DateTime date = appointmentDate.Add(appointmentTime);
            if (date.CompareTo(DateTime.Now) < 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns the first day of the week that the specified
        /// date is in using the current culture. 
        /// </summary>
        public DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetFirstDateOfWeek(dayInWeek, defaultCultureInfo);
        }

        /// <summary>
        /// Returns the first day of the week that the specified date 
        /// is in. 
        /// </summary>
        public DateTime GetFirstDateOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }
    }
}