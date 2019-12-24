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
    /// Interaction logic for InsertRequest.xaml
    /// </summary>
    public partial class InsertRequest : Window
    {
        public InsertRequest()
        {
            InitializeComponent();
            FillComboBoxes();
        }

        private void buttonInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connector connector = new Connector();
                connector.InsertIntoRequest(Convert.ToInt32(comboBoxDetail.SelectedValue), Convert.ToInt32(this.textBoxAmount.Text));
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
            DataTable dt = connector.GetComboBoxData("Деталь");

            comboBoxDetail.ItemsSource = dt.DefaultView;
            comboBoxDetail.DisplayMemberPath = "название";
            comboBoxDetail.SelectedValuePath = "id";
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
