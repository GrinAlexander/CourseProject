using System;
using System.Data;
using System.Windows;

namespace CourseProject.Inserts
{
    /// <summary>
    /// Interaction logic for InsertSell.xaml
    /// </summary>
    public partial class InsertSell : Window
    {
        public InsertSell()
        {
            InitializeComponent();
            this.dataPickerMain.SelectedDate = DateTime.Now;
            FillComboBoxes();
        }

        private void buttonInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connector connector = new Connector();
                connector.InsertSell(Convert.ToInt32(this.comboBoxRequest.SelectedValue), this.dataPickerMain.SelectedDate);
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
            DataTable dt = connector.GetTable("Заявка");

            comboBoxRequest.ItemsSource = dt.DefaultView;
            comboBoxRequest.DisplayMemberPath = "id";
            comboBoxRequest.SelectedValuePath = "id";
        }
    }
}
