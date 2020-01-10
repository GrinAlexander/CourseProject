using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

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
                Connector connector = new Connector();
                connector.InsertDetail
                    (
                    Convert.ToInt32(comboBoxStorage.SelectedValue),
                    Convert.ToInt32(comboBoxAuto.SelectedValue),
                    this.textBoxName.Text,
                    this.textBoxArticul.Text,
                    this.textBoxProd.Text,
                    this.textBoxCategory.Text,
                    float.Parse(this.textBoxPriceR.Text) + (float.Parse(this.textBoxPriceK.Text) / 100)
                    );
                MessageBox.Show("Запись успешно добавлена!");
        }

        public void FillComboBoxes()
        {
            Connector connector = new Connector();
            DataTable dt = connector.GetTable("Склад");
            DataTable dt_c = connector.GetTable("Авто");

            comboBoxStorage.ItemsSource = dt.DefaultView;
            comboBoxStorage.DisplayMemberPath = "адрес";
            comboBoxStorage.SelectedValuePath = "id";

            comboBoxAuto.ItemsSource = dt_c.DefaultView;
            comboBoxAuto.DisplayMemberPath = "модель";
            comboBoxAuto.SelectedValuePath = "id";
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void buttonAddCar_Click(object sender, RoutedEventArgs e)
        {
            InsertAuto insertAuto = new InsertAuto();
            insertAuto.ShowDialog();
            FillComboBoxes();
        }
    }
}
