using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfNursePlanning.Model;
using System.Windows.Controls;
using System.Windows.Input;
using System;
using System.Linq;

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
            dgr.SelectedIndex = 0;
            LoadData();
            //cbxRole.Items.Add("Utilisateur");
            //cbxRole.Items.Add("Admin");
            //cbxRole.Items.Add("Super Admin");

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
                        Id = item.Id,
                        LastName = item.LastName,
                        FirstName = item.FirstName,
                        BirthDay = item.BirthDay,
                        Adress = item.Adress,
                        SiretNumber = item.SiretNumber,
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
            dgr.ItemsSource = list;
        }
        

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                                e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null) return;

            Nurse nurse = dgr.SelectedItem as Nurse;
            txtLastName.Text = nurse.LastName;
            txtFirstName.Text = nurse.FirstName;
            dpBithDay.SelectedDate = nurse.BirthDay;
            txtAdress.Text = nurse.Adress;
            txtSiretNumber.Text = nurse.SiretNumber;
            txtPhoneNumber.Text = nurse.PhoneNumber;
            txtUseEmailrName.Text = nurse.UseEmailrName;
            txtUserName.Text = nurse.UserName;
            txtPasswordHash.Text = nurse.PasswordHash;
            txtSecurityStamp.Text = nurse.SecurityStamp;
            txtConcurrencyStamp.Text = nurse.ConcurrencyStamp;
            txtNormalizedUserName.Text = nurse.NormalizedUserName;
            txtEmail.Text = nurse.Email;
            txtNormalizedEmail.Text = nurse.NormalizedEmail;
            txtLockoutEnabled.Text = "true";
            txtPhoneNumberConfirmed.Text = "true";
            txtTwoFactorEnabled.Text = "true";
            txtEmailConfirmed.Text = "true";
            EnableDesable(true);
        }
      

        private void btnAppointement(object sender, RoutedEventArgs e)
        {

            Nurse nurse = dgr.SelectedItem as Nurse;
            AppointmentList rdvList = new AppointmentList(nurse.Id);
            rdvList.lblNurseName.Content = nurse.FirstName + " " + nurse.LastName;
          
            rdvList.Show();
            this.Close();


        }

        private void btnListPatient(object sender, RoutedEventArgs e)
        {
            PatientList patientList = new PatientList();
            patientList.Show();
            this.Close();

        }

        private void btnQuitter(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        void btnActiveNurse(object sender, RoutedEventArgs e)
        {
            Nurse nurse = dgr.SelectedItem as Nurse;

            nurse.IsActive = (bool)chkIsActive.IsChecked;
            nurse.LastName = txtLastName.Text;
            nurse.FirstName = txtFirstName.Text;
            nurse.BirthDay = (DateTime)dpBithDay.SelectedDate;
            nurse.Adress = txtAdress.Text;
            nurse.SiretNumber= txtSiretNumber.Text;
            nurse.LockoutEnabled = txtLockoutEnabled.Text == "true";
            nurse.PhoneNumberConfirmed = txtPhoneNumberConfirmed.Text == "true";
            nurse.TwoFactorEnabled = txtTwoFactorEnabled.Text == "true";
            nurse.EmailConfirmed = txtEmailConfirmed.Text == "true";
            nurse.AccessFailedCount = 0;
            nurse.PhoneNumber = txtPhoneNumber.Text;
            nurse.UseEmailrName = txtUseEmailrName.Text;
            nurse.UserName = txtUserName.Text;
            nurse.PasswordHash = txtPasswordHash.Text;
            nurse.SecurityStamp = txtSecurityStamp.Text;
            nurse.ConcurrencyStamp = txtConcurrencyStamp.Text;
            nurse.NormalizedUserName = txtNormalizedUserName.Text;
            nurse.Email = txtEmail.Text;
            nurse.NormalizedEmail = txtNormalizedEmail.Text;

            this.UpdateNurse(nurse);
        }

        private async void UpdateNurse(Nurse nurse)
        {
            await client.PutAsJsonAsync(API_URL + "/" + nurse.Id, nurse);
            MessageBox.Show("l'infirmier(e) " + nurse.LastName + " est modifié(e)");
            EnableDesable(false);
            dgr.Items.Refresh();

        }

        public void EnableDesable(bool i)
        {
            chkIsActive.IsEnabled = i;
            txtLastName.IsEnabled = i;
            txtFirstName.IsEnabled = i;
            dpBithDay.IsEnabled = i;
            txtAdress.IsEnabled = i;
            txtSiretNumber.IsEnabled = i;
            txtPhoneNumber.IsEnabled = i;
            txtUseEmailrName.IsEnabled = i;
            txtUserName.IsEnabled = i;
            txtEmail.IsEnabled = i;
            btnModifNurse.IsEnabled = i;
        }

    }
}