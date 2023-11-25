using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для PriceListWindow.xaml
    /// </summary>
    public partial class PriceListWindow : Window
    {
        private Response response;
        private HttpClient client;
        private PriceList? list;
        public PriceListWindow(Response response)
        {
            InitializeComponent();
            this.response = response;
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + response.access_token);
            Task.Run(() => Load());
        }
        private async Task Load()
        {
            List<PriceList>? list = await client.GetFromJsonAsync<List<PriceList>>("http://localhost:5174/api/pricelist");
            Dispatcher.Invoke(() =>
            {
                ListPrice.ItemsSource = null;
                ListPrice.Items.Clear();
                ListPrice.ItemsSource = list;
            });
        }
        private async Task Save()
        {
            PriceList priceList = new PriceList
            {
                Marka = Marka.Text,
                Complect = Complect.Text,
                Price = decimal.Parse(Price.Text)
            };
            JsonContent content = JsonContent.Create(priceList);
            using var response = await client.PostAsync("http://localhost:5174/api/pricelist", content);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Save();
        }

        private async void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            await Edit();
        }
        private async Task Edit()
        {
            list!.Marka = Marka.Text;
            list!.Complect = Complect.Text;
            list!.Price = decimal.Parse(Price.Text);
            JsonContent content = JsonContent.Create(list);
            using var response = await client.PutAsync("http://localhost:5174/api/pricelist", content);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
        private async void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            await Delete();
        }

        private void ListPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            list = ListPrice.SelectedItem as PriceList;
            Marka.Text = list?.Marka;
            Complect.Text= list?.Complect;
            Price.Text=list?.Price.ToString();
        }
        
        private async Task Delete()
        {
            using var response = await client.DeleteAsync("http://localhost:5174/api/pricelist/" + list?.Id);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
    }
}
