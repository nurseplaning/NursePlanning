using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using WpfNursePlanning.Model;
namespace WpfNursePlanning
{

    public partial class PatientList : Window
    {
        private const string API_URL = "https://localhost:44307/api/";
        private static HttpClient client = new HttpClient();
        static async Task<string> GetGlobalDataAsync()
        {
            var data = string.Empty;
            var response = await client.GetAsync(API_URL + "Patient");
            if (response.IsSuccessStatusCode)
                data = await response.Content.ReadAsStringAsync();

            return data;
        }
        public PatientList()
        {
            InitializeComponent();
            btnValidPatient.IsEnabled = true;
            LoadPatientData();
        }

        ObservableCollection<Patient> list = new ObservableCollection<Patient>();
        public async Task LoadPatientData()
        {
            var json = await GetGlobalDataAsync();
            var data = JObject.Parse(json).ToObject<List<Patient>>();
            foreach (var item in data)
            {
                list.Add(
                    new Patient
                    {
                        Id = item.Id,
                        LastName = item.LastName,
                        FirstName = item.FirstName,
                        BirthDay = item.BirthDay,
                        Adress = item.Adress,
                        SocialSecurityNumber = item.SocialSecurityNumber,
                    });
            }
            dgrPatient.ItemsSource = list;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
           
            Patient patient = dgrPatient.SelectedItem as Patient;
            if (patient.Id != "")
            {
                txtLastName.Text = patient.LastName;
                txtFirstName.Text = patient.FirstName;
                dpBithDay.SelectedDate = patient.BirthDay;
                txtAdress.Text = patient.Adress;
                txtSocialSecurityNumber.Text = patient.SocialSecurityNumber;
                btnValidPatient.IsEnabled = true;

                MessageBox.Show("aaaaaa");
                
            }

        }

        void btnActivePatient(object sender, RoutedEventArgs e)
        {
            Patient patient = new Patient();
            //patient.IsActive = true;
            //patient.LastName = txtLastName.Text;
            //patient.FirstName = txtFirstName.Text;
            //patient.BirthDay = (DateTime)dpBithDay.SelectedDate;
            //patient.Adress = txtAdress.Text;
            //patient.SocialSecurityNumber = txtSocialSecurityNumber.Text;

            patient.IsActive = true;
            patient.LastName = "AAAA";
            patient.FirstName = "xxxxxx";
           // patient.BirthDay = (DateTime)dpBithDay.SelectedDate;
            patient.Adress = "zzzzzz";
            patient.SocialSecurityNumber = "8884567898765";
            this.UpdatePatient(patient);
        }
        private async Task<Patient> UpdatePatient(Patient patient)
        {
            await client.PutAsJsonAsync("Patient/" + patient.Id, patient);
            HttpResponseMessage response = await client.PutAsJsonAsync(
        $"Patient/{patient.Id}", patient);
            response.EnsureSuccessStatusCode();
            patient = await response.Content.ReadAsAsync<Patient>();
            return patient;

        }


    }



}
