using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace CourseProject.Inserts
{
    /// <summary>
    /// Логика взаимодействия для InsertTeacher.xaml
    /// </summary>
    public partial class InsertOrder : Window
    {
        public InsertOrder()
        {
            InitializeComponent();
            FillComboBoxes();
        }

        private void buttonInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connector connector = new Connector();
                connector.InsertOrder(Convert.ToInt32(comboBoxDetail.SelectedValue), Convert.ToInt32(this.textBoxCount.Text));
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
