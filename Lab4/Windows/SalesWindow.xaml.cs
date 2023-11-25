using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для SalesWindow.xaml
    /// </summary>
    public partial class SalesWindow : Window
    {
        private MainWindow parent;
        private Response response;
        private HttpClient client;
        private Sales? sale;
        public SalesWindow(Response response, MainWindow autorize)
        {
            InitializeComponent();
            parent = autorize;
            this.response = response;
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + response.access_token);
            Task.Run(() => Load());
        }
        private async Task Load()
        {
            List<Sales>? list = await client.GetFromJsonAsync<List<Sales>>("http://localhost:5174/api/sales");
            foreach (Sales i in list!)
            {
                i.PriceList = await client.GetFromJsonAsync<PriceList>("http://localhost:5174/api/pricelist/" + i.PriceList_Id);
            }
            Dispatcher.Invoke(() =>
            {
                SalesList.ItemsSource = null;
                SalesList.Items.Clear();
                SalesList.ItemsSource = list;
            });
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            parent.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PriceListWindow window = new PriceListWindow(response);
            window.ShowDialog();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddEditSale addEditSale = new AddEditSale(response);
            if (addEditSale.ShowDialog() == true)
            {
                Sales sale = new Sales
                {
                    FIO = addEditSale.Fio,
                    DateSale = addEditSale.DateSale,
                    PriceList_Id = addEditSale.Marka!.Value,
                    Complect = addEditSale.ComplectAuto,
                    Discount = addEditSale.Discount,
                    TotalPrice = addEditSale.TotalPrice
                };
                await Save(sale);
            }
        }

        private async Task Save(Sales sale)
        {
            JsonContent content = JsonContent.Create(sale);
            using var response = await client.PostAsync("http://localhost:5174/api/sales", content);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }

        private async void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            await Delete();
        }
        private async Task Delete()
        {
            using var response = await client.DeleteAsync("http://localhost:5174/api/sale/" + sale?.Id);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }

        private void ListSales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sale=SalesList.SelectedItem as Sales;
        }

        private async void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AddEditSale addEditSale=new AddEditSale(response);
            addEditSale.Fio = sale.FIO;
            addEditSale.DateSale = sale.DateSale;
            addEditSale.Marka = sale.PriceList_Id;
            addEditSale.ComplectAuto = sale.Complect;
            addEditSale.Discount=sale.Discount;
            addEditSale.TotalPrice=sale.TotalPrice;
            if (addEditSale.ShowDialog() == true)
            {
                sale.FIO = addEditSale.Fio;
                sale.DateSale = addEditSale.DateSale;
                sale.PriceList_Id = addEditSale.Marka!.Value;
                sale.Complect = addEditSale.ComplectAuto;
                sale.Discount = addEditSale.Discount;
                sale.TotalPrice = addEditSale.TotalPrice;
                await Edit(sale);
            }
            
        }
        private async Task Edit(Sales sales)
        {
            JsonContent content = JsonContent.Create(sales);
            using var response = await client.PutAsync("http://localhost:5174/api/sale", content);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
    }
}
