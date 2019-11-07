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
using System.Text.RegularExpressions;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для InsertERBook.xaml
    /// </summary>
    public partial class InsertERBook : Window
    {
        public InsertERBook()
        {
            InitializeComponent();
        }

        private void buttonBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void buttonInsert_Click(object sender, RoutedEventArgs e)
        {
            Connector connector = new Connector();
            connector.InsertIntoERBook
                (
                dataPickerMain.SelectedDate,
                textBoxTopic.Text,
                textBoxMark.Text,
                Convert.ToInt32(textBoxIdTeacher.Text),
                Convert.ToInt32(textBoxIdStudent.Text)
                );
            MessageBox.Show("Запись успешно добавлена!");
        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
