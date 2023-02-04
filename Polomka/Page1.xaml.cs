using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Polomka
{
    public partial class Page1 : Page
    {
        private int start = 0;
        private int fullCount = 0;
        private int order = 0;
        private string gen = "";
        private string fnd = "";
        Frame fr = new Frame();

        public Page1(Frame frame)
        {
            fr = frame;
            InitializeComponent();
            List<Gender> genders = new List<Gender> { };
            genders = helper.GetContext().Gender.ToList();
            genders.Add(new Gender { Code = "Все" });
            Gender.ItemsSource = genders.OrderBy(Gender => Gender.Code);
            Load();
        }

        public void Load()
        {
            fullCount = helper.GetContext().Client.Count();
            full.Text = fullCount.ToString();
            int ost = fullCount % 10;
            int pag = (fullCount - ost) / 10;

            if (ost > 0) pag++;

            pagin.Children.Clear();

            for (int i = 0; i < pag; i++)
            {
                Button myButton = new Button();
                myButton.Height = 30;
                myButton.Content = i + 1;
                myButton.Width = 20;
                myButton.HorizontalAlignment = HorizontalAlignment.Center;
                myButton.Tag = i;
                myButton.Click += new RoutedEventHandler(paginButto_Click); ;
                pagin.Children.Add(myButton);
            }

            try
            {
                var ag = helper.GetContext().Client.Where(Client => Client.LastName.Contains(fnd) || Client.Phone.Contains(fnd) || Client.Email.Contains(fnd));
                if (gen == "")
                {
                    fullCount = ag.Count();
                    if (order == 0) agentGrid.ItemsSource = ag.OrderBy(Client => Client.ID).Skip(start * 10).Take(10).ToList();
                    if (order == 1) agentGrid.ItemsSource = ag.OrderBy(Client => Client.LastName).Skip(start * 10).Take(10).ToList();
                    if (order == 2) agentGrid.ItemsSource = ag.OrderByDescending(Client => Client.LastName).Skip(start * 10).Take(10).ToList();
                    if (order == 3) agentGrid.ItemsSource = ag.OrderBy(Client => Client.RegistrationDate).Skip(start * 10).Take(10).ToList();
                    if (order == 4) agentGrid.ItemsSource = ag.OrderByDescending(Client => Client.RegistrationDate).Skip(start * 10).Take(10).ToList();
                }
                else
                {
                    var agg = ag.Where(Client => Client.Gender.Code == gen);
                    fullCount = agg.Count();
                    if (order == 0) agentGrid.ItemsSource = agg.OrderBy(Client => Client.ID).Skip(start * 10).Take(10).ToList();
                    if (order == 1) agentGrid.ItemsSource = agg.OrderBy(Client => Client.LastName).Skip(start * 10).Take(10).ToList();
                    if (order == 2) agentGrid.ItemsSource = agg.OrderByDescending(Client => Client.LastName).Skip(start * 10).Take(10).ToList();
                    if (order == 3) agentGrid.ItemsSource = agg.OrderBy(Client => Client.RegistrationDate).Skip(start * 10).Take(10).ToList();
                    if (order == 4) agentGrid.ItemsSource = agg.OrderByDescending(Client => Client.RegistrationDate).Skip(start * 10).Take(10).ToList();
                }
            }
            catch
            {
                return;
            };


        }

        private void turnButton()
        {
            if (start == 0) 
            { back.IsEnabled = false; }
            else { back.IsEnabled = true; };
            if ((start + 1) * 10 > fullCount) { forward.IsEnabled = false; }
            else { forward.IsEnabled = true; };
        }


        private void paginButto_Click(object sender, RoutedEventArgs e)
        {
            start = Convert.ToInt32(((Button)sender).Tag.ToString());
            Load();
        }


        private void updateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            fr.Content = new Page2(null);
        }

        private void revButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void agentGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }

        private void agentGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (agentGrid.SelectedItems.Count > 0)
            {
                Client agent = agentGrid.SelectedItems[0] as Client;

                if (agent != null)
                {
                    fr.Content = new Page2(agent);
                }
            }

        }

        private void forward_Click(object sender, RoutedEventArgs e)
        {
            start++;
            turnButton();
            Load();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            start--;
            turnButton();
            Load();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            order = Convert.ToInt32(selectedItem.Tag.ToString());
            Load();
        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gen = ((Gender)Gender.SelectedItem).Code;
            Load();
        }

        private void posButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            fnd = ((TextBox)sender).Text;
            Load();
        }

        private void agentGrid_LoadingRow_1(object sender, DataGridRowEventArgs e)
        {
            Client client = (Client)e.Row.DataContext;
            var CountMeeting = helper.GetContext().ClientService.Where(x => client.ID == x.ClientID).ToList().Count;
            var LastMeeting = helper.GetContext().ClientService.OrderByDescending(x => x.StartTime).Where(x => client.ID == x.ClientID).FirstOrDefault();
            if (CountMeeting != 0)
            {
                client.Meeting = "Количество посещений: " + CountMeeting.ToString();
                client.DateTime = "Последнее посещение: " + LastMeeting.StartTime.ToString();
            }
        }
    }
}
