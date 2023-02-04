using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Polomka
{
    public partial class Page2 : Page
    {
        Client client;
        public Page2(Client ag)
        {
            InitializeComponent();
            try
            {
                Gender.ItemsSource = helper.GetContext().Gender.ToList();
                product.ItemsSource = helper.GetContext().Product.ToList();
            }
            catch { };
            if (ag != null)
            {
                client = ag;
                Gender.SelectedItem = ag.Gender;
                this.Name.Text = ag.FirstName;
                this.LastName.Text = ag.LastName;
                this.Patronymic.Text = ag.Patronymic;
                this.Birthday.Text = ag.Birthday.ToString();
                this.RegDate.Text = ag.RegistrationDate.ToString();
                this.Email.Text = ag.Email;
                this.Phone.Text = ag.Phone;
                historyGrid.ItemsSource = helper.GetContext().ProductSale.Where(Client => Client.ClientService.ClientID == ag.ID).ToList();
            }
            else
            {
                client = new Client();
                btnDelAg.IsEnabled = false;
                btnWritHi.IsEnabled = false;
                btnDelHi.IsEnabled = false;
            }
            this.DataContext = client;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
          
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void historyGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void product_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
