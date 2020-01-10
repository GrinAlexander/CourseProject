using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

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
                connector.InsertRequest(Convert.ToInt32(comboBoxDetail.SelectedValue), Convert.ToInt32(this.textBoxAmount.Text));
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
            DataTable dt = connector.GetTable("Деталь");

            comboBoxDetail.ItemsSource = dt.DefaultView;
            comboBoxDetail.DisplayMemberPath = "артикул";
            comboBoxDetail.SelectedValuePath = "id";
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
