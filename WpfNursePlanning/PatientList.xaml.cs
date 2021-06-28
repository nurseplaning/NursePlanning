using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfNursePlanning.Model;
namespace WpfNursePlanning
{

    public partial class PatientList : Window
    {
        private const string API_URL = "https://localhost:44307/api/Patient";
        private static HttpClient client = new HttpClient();
        static async Task<string> GetGlobalDataAsync()
        {
            var data = string.Empty;
            var response = await client.GetAsync(API_URL);
            if (response.IsSuccessStatusCode)
                data = await response.Content.ReadAsStringAsync();

            return data;
        }
        public PatientList()
        {
            InitializeComponent();
            btnValidPatient.IsEnabled = false;
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
                txtmailurl.Text = patient.UseEmailrName;

                txtUserName.Text = patient.UserName;
                txtPhoneNumber.Text = patient.PhoneNumber;
                txtPasswordHash.Text = patient.PasswordHash;
                txtSecurityStamp.Text = patient.SecurityStamp;
                txtConcurrencyStamp.Text = patient.ConcurrencyStamp;
                txtNormalizedUserName.Text = patient.NormalizedUserName;
                txtEmail.Text = patient.Email;
                txtNormalizedEmail.Text = patient.NormalizedEmail;


                //  txtLockoutEnabled.Text = ;
                // txtPhoneNumberConfirmed.Text = patient.PhoneNumberConfirmed;
                //txtTwoFactorEnabled.Text = "true";
                //  txtEmailConfirmed.Text = "true";
                //  AccessFailedCount.Text = 1;



            }

        }

        void btnActivePatient(object sender, RoutedEventArgs e)
        {
            Patient patient = dgrPatient.SelectedItem as Patient;

            patient.IsActive = true;


            patient.LastName = txtLastName.Text;
            patient.FirstName = txtFirstName.Text;
            patient.BirthDay = (DateTime)dpBithDay.SelectedDate;
            patient.Adress = txtAdress.Text;
            patient.SocialSecurityNumber = txtSocialSecurityNumber.Text;
            patient.UseEmailrName = txtmailurl.Text;

            patient.UserName = txtUserName.Text;
            patient.PhoneNumber = txtPhoneNumber.Text;
            patient.PasswordHash = txtPasswordHash.Text;
            patient.SecurityStamp = txtSecurityStamp.Text;
            patient.ConcurrencyStamp = txtConcurrencyStamp.Text;
            patient.NormalizedUserName = txtNormalizedUserName.Text;
            patient.Email = txtEmail.Text;
            patient.NormalizedEmail = txtNormalizedEmail.Text;

            patient.LockoutEnabled = txtLockoutEnabled.Text == "true";
            patient.PhoneNumberConfirmed = txtPhoneNumberConfirmed.Text == "true";
            patient.TwoFactorEnabled = txtTwoFactorEnabled.Text == "true";
            patient.EmailConfirmed = txtEmailConfirmed.Text == "true";
            patient.AccessFailedCount = 1;





            //patient.LastName = "AAAA";
            //patient.FirstName = "xxxxxx";
            //patient.BirthDay = DateTime.Now;
            //patient.Adress = "zzzzzz";
            //patient.SocialSecurityNumber = "8884567898765";
            //patient.UseEmailrName = "aaaa@aaaa.com";
            //patient.UserName = "aaaa@aaaa.com";
            //patient.PhoneNumber = "0665473261";
            //patient.PasswordHash = "hkhdjkshdj";
            //patient.PhoneNumberConfirmed = true;
            //patient.EmailConfirmed = true;
            //patient.SecurityStamp = "26JHD4JIVDZ7A3BQDQHAUZPQKZYYIQEI";
            //patient.ConcurrencyStamp = "0d5c7129-3889-4341-9fb5-db044ceac2bc";
            //patient.TwoFactorEnabled = false;
            //patient.NormalizedUserName = "ESTOYMALITO@PATIENT.FR";
            //patient.Email = "estoymalito@patient.fr";
            //patient.NormalizedEmail = "ESTOYMALITO@PATIENT.FR";
            //patient.LockoutEnabled = true;
            //patient.AccessFailedCount = 0;

            this.UpdatePatient(patient);
        }
        private async void UpdatePatient(Patient patient)
        {
            await client.PutAsJsonAsync(API_URL + "/" + patient.Id, patient);
            MessageBox.Show("le patient(e) " + patient.LastName + " est désactivé(e)");

        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainList = new MainWindow();
            mainList.Show();
            this.Close();
        }


    }



}
