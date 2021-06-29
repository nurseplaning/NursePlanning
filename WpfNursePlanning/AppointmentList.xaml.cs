using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfNursePlanning.Model;
using System.Linq;

namespace WpfNursePlanning
{

    public partial class AppointmentList : Window
    {
        private const string API_URL = "https://localhost:44307/api/Appointments";
        private static HttpClient client = new HttpClient();
        static async Task<string> GetAppointmentAsync(string Nurseid)
        {
            var data = string.Empty;
            var response = await client.GetAsync(API_URL + "/nurse/" + Nurseid);

            if (response.IsSuccessStatusCode)
                data = await response.Content.ReadAsStringAsync();

            return data;
        }
        public AppointmentList(string nurseId)
        {
            InitializeComponent();
            LoadAppointmentData(nurseId);
        }

        ObservableCollection<Appointment> list = new ObservableCollection<Appointment>();
        public async Task LoadAppointmentData(string nurseId)
        {
            MainWindow main = new MainWindow();
            var json = await GetAppointmentAsync(nurseId);
            var data = JObject.Parse(json).ToObject<List<Appointment>>();
            foreach (var item in data)
            {
                list.Add(
                    new Appointment
                    {
                        Date = item.Date,
                        AtHome = item.AtHome,
                        Patient = item.Patient,
                        Status = item.Status,
                        Nurse = item.Nurse,
                        NurseId = item.NurseId,
                        //Name = item.Nurse.LastName, //+ " " + item.Nurse.FirstName
                    }) ; 
            }
            dgrRdv.ItemsSource = list;
        }

        void btnDetailPatient(object sender, RoutedEventArgs e)
        {
            Appointment appointment = dgrRdv.SelectedItem as Appointment;
            txtLastName.Text = appointment.Patient.LastName;
            txtFirstName.Text = appointment.Patient.FirstName;
            dpBithDay.SelectedDate = appointment.Patient.BirthDay;
            txtAdress.Text = appointment.Patient.Adress;
            txtSsNumber.Text = appointment.Patient.SocialSecurityNumber;
            txtPhoneNumber.Text = appointment.Patient.PhoneNumber;
            txtEmail.Text = appointment.Patient.Email;
            chkIsActiveRdv.IsChecked = appointment.Patient.IsActive;
            
        }
            
        private void btnRetourRdv(object sender, RoutedEventArgs e)
        {
            MainWindow mainList = new MainWindow();
            mainList.Show();
            this.Close();
        }

        
    }
}
