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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AutokForm.xaml
    /// </summary>
    public partial class AutokForm : Window
    {
        AutokService service = new AutokService();
        Autok auto;
        public AutokForm()
        {
            InitializeComponent();
        }

        public AutokForm(Autok autok)
        {
            InitializeComponent();
            this.auto = autok;
            LoadAuto();
            addButton.Visibility = Visibility.Collapsed;
            updateButton.Visibility = Visibility.Visible;
        }

        private void LoadAuto()
        {
            azonInput.Text = this.auto.azonosito;
            elerhetoInput.IsChecked = this.auto.elerheto;
            markaInput.Text = this.auto.marka;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Autok auto = CreateAutoFromInputFields();

                Autok newAuto = service.Add(auto);
                if (newAuto.Id != 0)
                {
                    MessageBox.Show("Sikeres felvétel");
                    azonInput.Text = "";
                    elerhetoInput.IsChecked = false;
                    markaInput.Text = "";
                }
                else
                {
                    MessageBox.Show("Hiba történt a felvétel során!");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Autok auto = CreateAutoFromInputFields();
                Autok updated = service.Update(this.auto.Id, auto);
                if (updated.Id != 0)
                {
                    MessageBox.Show("Sikeres módosítás");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hiba történt a módosítás során!");
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        private Autok CreateAutoFromInputFields()
        {
            string azon = azonInput.Text.Trim();
            bool elerheto = (bool)elerhetoInput.IsChecked;
            string marka = markaInput.Text.Trim();

            if (string.IsNullOrEmpty(azon))
            {
                throw new Exception("Azonosító megadása kötelező");
            }
            if (string.IsNullOrEmpty(marka))
            {
                throw new Exception("A márka kitőltése kötelező");
            }
            if (!(azon[0]=='S' && azon[1]=='K' && azon[2]=='U' && azon[3]=='_' && azon.Length < 7))
            {
                throw new Exception("Az azonosító formátuma nem megfelelő.");
            }

            Autok auto = new Autok();
            auto.azonosito = azon;
            auto.elerheto = elerheto;
            auto.marka = marka;
            return auto;
        }
    }
}
