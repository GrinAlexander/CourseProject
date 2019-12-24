using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace CourseProject.Inserts
{
    /// <summary>
    /// Логика взаимодействия для InsertStudent.xaml
    /// </summary>
    public partial class InsertDetail : Window
    {
        public InsertDetail()
        {
            InitializeComponent();
            FillComboBoxes();
        }

        private void buttonInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connector connector = new Connector();
                connector.InsertIntoDetail
                    (
                    Convert.ToInt32(comboBoxStorage.SelectedValue),
                    this.textBoxName.Text,
                    this.textBoxProd.Text,
                    this.textBoxCategory.Text,
                    float.Parse(this.textBoxPriceR.Text) + (float.Parse(this.textBoxPriceK.Text) / 100)
                    );
                MessageBox.Show("Запись успешно добавлена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void FillComboBoxes()
        {
            Connector connector = new Connector();
            DataTable dt = connector.GetComboBoxData("Склад");

            comboBoxStorage.ItemsSource = dt.DefaultView;
            comboBoxStorage.DisplayMemberPath = "адрес";
            comboBoxStorage.SelectedValuePath = "id";
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
