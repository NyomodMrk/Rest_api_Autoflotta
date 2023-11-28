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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AutokService service = new AutokService();
        public MainWindow()
        {
            InitializeComponent();
            AutokTable.ItemsSource = service.GetAll();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AutokForm autokForm = new AutokForm();
            autokForm.Closed += (_, _) =>
            {
                AutokTable.ItemsSource = service.GetAll();
            };
            autokForm.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Autok selected = AutokTable.SelectedItem as Autok;
            if (selected == null)
            {
                MessageBox.Show("Törléshez előbb válasszon ki elemet!");
                return;
            }
            MessageBoxResult clickedButton = MessageBox.Show($"Biztos hogy törölni szeretné az alábbi elemet: {selected.azonosito}", "Törlés", MessageBoxButton.YesNo);
            if (clickedButton == MessageBoxResult.Yes)
            {
                if (service.Delete(selected))
                {
                    MessageBox.Show("Sikeres törlés!");
                }
                else
                {
                    MessageBox.Show("Hiba történt a törlés során!");
                }
                AutokTable.ItemsSource = service.GetAll();
            }

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Autok selected = AutokTable.SelectedItem as Autok;
            if (selected == null)
            {
                MessageBox.Show("Módosításhoz előbb válasszon ki elemet!");
                return;
            }
            AutokForm autokForm = new AutokForm(selected);
            autokForm.Closed += (_, _) =>
            {
                AutokTable.ItemsSource = service.GetAll();
            };
            autokForm.ShowDialog();
        }
    }
}
