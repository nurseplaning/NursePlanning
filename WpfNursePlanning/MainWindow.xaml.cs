using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfNursePlanning.Model;
using System.Windows.Controls;

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

                    });

            }
            dgr.ItemsSource = list;
        }


        

        void btnEditNurse(object sender, RoutedEventArgs e)
        {
            Nurse nurse = ((FrameworkElement)sender).DataContext as Nurse;
            txtLastName.Text = nurse.LastName;
            txtFirstName.Text = nurse.FirstName;
            dpBithDay.Content = nurse.BirthDay;
            txtAdress.Text = nurse.Adress;
            txtSiretNumber.Text = nurse.SiretNumber;

        }

        void btnDeleteNurse(object sender, RoutedEventArgs e)
        {
            Nurse nurse = ((FrameworkElement)sender).DataContext as Nurse;
            this.DeleteNurses(nurse.Id);
        }
        private async void DeleteNurses(string id)
        {
            await client.DeleteAsync("/" + id);
        }
    }
}