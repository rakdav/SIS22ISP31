using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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
using Lab4.Models;

namespace Lab4
{
    /// <summary>
    /// Логика взаимодействия для AddEditSale.xaml
    /// </summary>
    public partial class AddEditSale : Window
    {
        private Response response;
        private HttpClient client;
        private List<PriceList>? list;
        public AddEditSale(Response response)
        {
            InitializeComponent();
            this.response = response;
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + response.access_token);
            Task.Run(() => Load());
        }

        private async Task Load()
        {
             list = await client.GetFromJsonAsync<List<PriceList>>("http://localhost:5174/api/pricelist");
            Dispatcher.Invoke(() =>
            {
                cbMarka.ItemsSource = null;
                cbMarka.Items.Clear();
                cbMarka.ItemsSource = list?.Select(p=>p.Marka);
            });
        }

        private void cbMarka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbComplect.Content = list?.Where(p => p.Marka == cbMarka.SelectedItem.ToString()).FirstOrDefault()?.Complect;
            lbPrice.Content= list?.Where(p => p.Marka == cbMarka.SelectedItem.ToString()).FirstOrDefault()?.Price;
        }

        private void tbDiscounr_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cbMarka.SelectedItem != null)
            {
                decimal? price = list?.Where(p => p.Marka == cbMarka.SelectedItem.ToString()).FirstOrDefault()?.Price;
                if (lbPrice.Content.ToString()?.Length != 0)
                    try
                    {
                        lbPrice.Content = price - price * (int.Parse(tbDiscounr.Text)) / 100;
                    }
                    catch (Exception ex)
                    {

                    }
            }
        }

        public string Fio { 
            get
            {
                return FIO.Text;
            }
            set
            {
                FIO.Text = value;
            }
        }
        public DateTime DateSale
        {
            get
            {
                return DatePicker.SelectedDate!.Value;
            }
            set
            {
                DatePicker.SelectedDate = value;
            }
        }
        public int? Marka
        {
            get => list?.Where(p => p.Marka == cbMarka.SelectedItem.ToString()).FirstOrDefault()?.Id;
            set
            {
                int? id = value;
                PriceList sale= client.GetFromJsonAsync<PriceList>("http://localhost:5174/api/pricelist/"+id).Result;
                cbMarka.SelectedValue = sale.Marka; 
            }
        }
        public string? ComplectAuto
        {
            get { return lbComplect.Content.ToString(); }
            set
            {
                 lbComplect.Content = value;
            }
        }
        public int Discount
        {
            get { return int.Parse(tbDiscounr.Text); }
            set { tbDiscounr.Text = value.ToString(); }
        }
        public decimal? TotalPrice
        {
            get { return decimal.Parse(lbPrice.Content.ToString()); }
            set
            {
                lbPrice.Content = value.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
