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


namespace WpfNursePlanning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string API_URL = "https://localhost:44307/api/Nurses";
        private static HttpClient client;

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
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditNurse editNurse = new EditNurse();
            editNurse.Show();
            this.Close();

        }
        ObservableCollection<Nurse> list = new ObservableCollection<Nurse>();
        public async Task LoadData()
        {
            client = new HttpClient();
            var json = await GetGlobalDataAsync();
            var data = JObject.Parse(json).ToObject<List<Nurse>>();

            foreach (var item in data)
            {
                list.Add(new Nurse { LastName = item.LastName, FirstName = item.FirstName });
            }
            dgr.ItemsSource = list;



        }



    }
}


