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

       // static async Task<string> GetAppointmentAsync()
         static async Task<string> GetAppointmentAsync(string Nurseid)
        {
            var data = string.Empty;
           var response = await client.GetAsync(API_URL + "/nurse/" + Nurseid);
            //var response = await client.GetAsync(API_URL);
            if (response.IsSuccessStatusCode)
                data = await response.Content.ReadAsStringAsync();

            return data;

        }
        public AppointmentList()
        {
            InitializeComponent();
            LoadAppointmentData();
        }

        ObservableCollection<Appointment> list = new ObservableCollection<Appointment>();
        public async Task LoadAppointmentData()
        {
            MainWindow main = new MainWindow();
            
            string Id = (string)lblPatient.Content;

            var json = await GetAppointmentAsync(Id);
            //var json = await GetAppointmentAsync();
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
                    });

            }
           dgrRdv.ItemsSource = list;
           // dgrRdv.ItemsSource = list.Where(n => n.NurseId == lblPatient.Content);
            
            
        }
    }
}
