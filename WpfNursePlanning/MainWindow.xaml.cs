using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfNursePlanning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<MyModel> list = new ObservableCollection<MyModel>();

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditNurse editNurse = new EditNurse();
            editNurse.Show();
            this.Close();

        }
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            list.Add(new MyModel { Id = 123, LastName = "aaaa", FirstName = "aaaaaa" });
            list.Add(new MyModel { Id = 456, LastName = "bbbbb", FirstName = "bbbb" });
            list.Add(new MyModel { Id = 789, LastName = "ccccc", FirstName = "cccccc" });
            this.dgr.ItemsSource = list;
        }


        private void LoadData()
        {


        }

        public class MyModel
        {
            public int Id { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }



        }




    }
}


