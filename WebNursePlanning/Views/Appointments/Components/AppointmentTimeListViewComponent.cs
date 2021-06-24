using Dal;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebNursePlanning.Shared.Components
{
    public class AppointmentTimeListViewComponent : ViewComponent
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentTimeListViewComponent(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {

            Dictionary<string, List<TimeSpan>> dico = await GetListAvailableAppointments(id);

            ViewData["AppointmentsAvailables"] = dico;
            return View();
        }

        public async Task<Dictionary<string, List<TimeSpan>>> GetListAvailableAppointments(string Personid)
        {
            //Define start time and end time for taking appointments
            TimeSpan startTime = new(8, 0, 0);
            TimeSpan endTime = new(17, 30, 0);
            DateTime today = DateTime.Now;
            DateTime dateOfWeek = GetFirstDayOfWeek(today);
            TimeSpan delayAppointment = new(0, 30, 0);

            //Get Total minutes of a day
            TimeSpan timeInterval = endTime.Subtract(startTime);
            List<TimeSpan> listTimes = new();
            //Calculate how many time slots  of appointments in a day
            double nbAppointments = timeInterval.Divide(delayAppointment);
            var listAppointments = await _appointmentRepository.ListAppointmentsById(Personid);
            //Create List of Appointments
            Dictionary<string, List<TimeSpan>> dicoAppointments = new();
            for (int day = 0; day < 7; day++)
            {
                
                for  (int timeappointment = 0; timeappointment < nbAppointments; timeappointment++)
                {
                    if (CheckAvailabilityAppointment(listAppointments, dateOfWeek, startTime) && !IsPast(dateOfWeek, startTime))
                        listTimes.Add(startTime);
                    else
                        listTimes.Add(new TimeSpan());


                    startTime = startTime.Add(delayAppointment);
                }
                if (!dicoAppointments.ContainsKey(dateOfWeek.ToString("dddd dd MMMM yyyy")))
                    dicoAppointments.Add(dateOfWeek.ToString("dddd dd MMMM yyyy"), listTimes);

                //Preparation des variables pour le jour suivant
                listTimes = new List<TimeSpan>();
                dateOfWeek = dateOfWeek.AddDays(1);
                startTime = new TimeSpan(8, 0, 0);
            }
            return dicoAppointments;
        }

        public bool CheckAvailabilityAppointment(IEnumerable<Appointment> appointments, DateTime appointmentDay, TimeSpan appointmentTime)
        {
            bool isAvailable = false;
            foreach (var item in appointments)
            {
                if (item.Date.Day == appointmentDay.Day && item.Date.Hour == appointmentTime.Hours && item.Date.Minute == appointmentTime.Minutes)
                    isAvailable = false;
                else
                    isAvailable = true;
            }
            return isAvailable;
        }

        public bool IsPast(DateTime appointmentDate, TimeSpan appointmentTime)
        {
            DateTime date = appointmentDate.Add(appointmentTime);
            if (date.CompareTo(DateTime.Now.Date) < 0)
                return true;
            else 
                return false;
        }

        /// <summary>
        /// Returns the first day of the week that the specified
        /// date is in using the current culture. 
        /// </summary>
        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetFirstDateOfWeek(dayInWeek, defaultCultureInfo);
        }

        /// <summary>
        /// Returns the first day of the week that the specified date 
        /// is in. 
        /// </summary>
        public static DateTime GetFirstDateOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }
    }
}
