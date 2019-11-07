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
    /// Логика взаимодействия для InsertStudent.xaml
    /// </summary>
    public partial class InsertStudent : Window
    {
        public InsertStudent()
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
            connector.InsertIntoStudents(this.textBoxGroup.Text, this.textBoxSurame.Text, this.textBoxName.Text, this.textBoxPatronymic.Text);
            MessageBox.Show("Запись успешно добавлена!");
        }
    }
}
