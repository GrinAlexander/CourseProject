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

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для InsertTeacher.xaml
    /// </summary>
    public partial class InsertTeacher : Window
    {
        public InsertTeacher()
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
            connector.InsertIntoTeachers(textBoxSubject.Text, textBoxSurame.Text, textBoxName.Text, textBoxPatronymic.Text);
            MessageBox.Show("Запись успешно добавлена!");
        }
    }
}
