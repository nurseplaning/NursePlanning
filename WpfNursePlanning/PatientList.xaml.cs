using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            dgrPatient.SelectedIndex = 0;
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
                        IsActive = item.IsActive,
                        UseEmailrName = item.UseEmailrName,
                        UserName = item.UserName,
                        PasswordHash = item.PasswordHash,
                        SecurityStamp = item.SecurityStamp,
                        ConcurrencyStamp = item.ConcurrencyStamp,
                        NormalizedUserName = item.NormalizedUserName,
                        Email = item.Email,
                        NormalizedEmail = item.NormalizedEmail,
                        PhoneNumber = item.PhoneNumber,
                        LockoutEnabled = item.LockoutEnabled,
                        PhoneNumberConfirmed = item.PhoneNumberConfirmed,
                        TwoFactorEnabled = item.TwoFactorEnabled,
                        EmailConfirmed = item.TwoFactorEnabled,

                    });
            }

            dgrPatient.ItemsSource = list;
        }


        void btnActivePatient(object sender, RoutedEventArgs e)
        {
            Patient patient = dgrPatient.SelectedItem as Patient;

            patient.IsActive = (bool)chkIsActive.IsChecked;
            patient.LastName = txtLastName.Text;
            patient.FirstName = txtFirstName.Text;
            patient.BirthDay = (DateTime)dpBithDay.SelectedDate;
            patient.Adress = txtAdress.Text;
            patient.SocialSecurityNumber = txtSocialSecurityNumber.Text;
            patient.LockoutEnabled = txtLockoutEnabled.Text == "true";
            patient.PhoneNumberConfirmed = txtPhoneNumberConfirmed.Text == "true";
            patient.TwoFactorEnabled = txtTwoFactorEnabled.Text == "true";
            patient.EmailConfirmed = txtEmailConfirmed.Text == "true";
            patient.AccessFailedCount = 0;
            patient.PhoneNumber = txtNumTelephone.Text;
            patient.UseEmailrName = txtUseEmailrName.Text;
            patient.UserName = txtUserName.Text;
            patient.PasswordHash = txtPasswordHash.Text;
            patient.SecurityStamp = txtSecurityStamp.Text;
            patient.ConcurrencyStamp = txtConcurrencyStamp.Text;
            patient.NormalizedUserName = txtNormalizedUserName.Text;
            patient.Email = txtEmail.Text;
            patient.NormalizedEmail = txtNormalizedEmail.Text;

            this.UpdatePatient(patient);
        }
        private async void UpdatePatient(Patient patient)
        {
            await client.PutAsJsonAsync(API_URL + "/" + patient.Id, patient);
            MessageBox.Show("le/la patient(e) " + patient.LastName + " est modifié(e)");
            EnableDesable(false);
            dgrPatient.Items.Refresh();
            
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainList = new MainWindow();
            mainList.Show();
            this.Close();
        }
        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                                e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null) return;
            Patient patient = dgrPatient.SelectedItem as Patient;

            chkIsActive.IsChecked = patient.IsActive;
            txtLastName.Text = patient.LastName;
            txtFirstName.Text = patient.FirstName;
            dpBithDay.SelectedDate = patient.BirthDay;
            txtAdress.Text = patient.Adress;
            txtSocialSecurityNumber.Text = patient.SocialSecurityNumber;
            txtNumTelephone.Text = patient.PhoneNumber;
            txtUseEmailrName.Text = patient.UseEmailrName;
            txtUserName.Text = patient.UserName;
            txtPasswordHash.Text = patient.PasswordHash;
            txtSecurityStamp.Text = patient.SecurityStamp;
            txtConcurrencyStamp.Text = patient.ConcurrencyStamp;
            txtNormalizedUserName.Text = patient.NormalizedUserName;
            txtEmail.Text = patient.Email;
            txtNormalizedEmail.Text = patient.NormalizedEmail;
            txtLockoutEnabled.Text = "true";
            txtPhoneNumberConfirmed.Text = "true";
            txtTwoFactorEnabled.Text = "true";
            txtEmailConfirmed.Text = "true";

            EnableDesable(true);
        }
        public void EnableDesable(bool i)
        {
            chkIsActive.IsEnabled = i;
            txtLastName.IsEnabled = i;
            txtFirstName.IsEnabled = i;
            dpBithDay.IsEnabled = i;
            txtAdress.IsEnabled = i;
            txtSocialSecurityNumber.IsEnabled = i;
            txtNumTelephone.IsEnabled = i;
            txtUseEmailrName.IsEnabled = i;
            txtUserName.IsEnabled = i;
            txtEmail.IsEnabled = i;
            btnValidPatient.IsEnabled = i;
        }

        
    }
}
