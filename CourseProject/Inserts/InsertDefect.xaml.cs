using System;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Data;

namespace CourseProject.Inserts
{
    /// <summary>
    /// Логика взаимодействия для InsertERBook.xaml
    /// </summary>
    public partial class InsertDefect : Window
    {
        public InsertDefect()
        {
            InitializeComponent();
            FillComboBoxes();
        }

        private void buttonInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connector connector = new Connector();
                connector.InsertDefect(Convert.ToInt32(comboBoxDetail.SelectedValue), this.textBoxDescription.Text);
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
