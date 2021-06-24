using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfNursePlanning
{
   
    public partial class EditNurse : Window
    {
        private const string API_URL = "https://coronavirusapi-france.now.sh/AllLiveData";
        //private const string API_URL = "https://localhost:44307/api/Nurses";
        public EditNurse()
        {
            InitializeComponent();
            cbxRole.Items.Add("Utilisateur");
            cbxRole.Items.Add("Admin");
            cbxRole.Items.Add("Super Admin");
        }

        private void btnValidModif(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
