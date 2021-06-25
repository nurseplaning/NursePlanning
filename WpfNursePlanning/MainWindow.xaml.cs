using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfNursePlanning.Model;

namespace WpfNursePlanning
{

    public partial class MainWindow : Window
    {
        private const string API_URL = "https://localhost:44307/api/Nurses";
        private static HttpClient client = new HttpClient();
        static async Task<string> GetGlobalDataAsync()
        {
            var data = string.Empty;
            var response = await client.GetAsync(API_URL);
            if (response.IsSuccessStatusCode)
                data = await response.Content.ReadAsStringAsync();

            return data;
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            cbxRole.Items.Add("Utilisateur");
            cbxRole.Items.Add("Admin");
            cbxRole.Items.Add("Super Admin");
        }

        ObservableCollection<Nurse> list = new ObservableCollection<Nurse>();
        public async Task LoadData()
        {
            var json = await GetGlobalDataAsync();
            var data = JObject.Parse(json).ToObject<List<Nurse>>();
            foreach (var item in data)
            {
                list.Add(
                    new Nurse   
                    {
                       Id=item.Id,
                        LastName = item.LastName,
                        FirstName = item.FirstName,
                        BirthDay = item.BirthDay,
                        Adress = item.Adress,
                        SiretNumber = item.SiretNumber,
                    });
            }
            dgr.ItemsSource = list;
        }

        void btnEditNurse(object sender, RoutedEventArgs e) 
        {
            Nurse nurse = dgr.SelectedItem as Nurse;
            txtLastName.Text = nurse.LastName;
            txtFirstName.Text = nurse.FirstName;
            dpBithDay.SelectedDate = nurse.BirthDay;
            txtAdress.Text = nurse.Adress;  
            txtSiretNumber.Text = nurse.SiretNumber;
        }

        private void btnAppointement(object sender, RoutedEventArgs e)
        {
            Nurse nurse = dgr.SelectedItem as Nurse; 
            AppointmentList rdvList = new AppointmentList(nurse.Id);
            rdvList.Show();
        }

        private void btnListPatient(object sender, RoutedEventArgs e)
        {
            PatientList patientList = new PatientList();
            patientList.Show();
        }
    }
}