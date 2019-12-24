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
    /// Логика взаимодействия для DBSettings.xaml
    /// </summary>
    public partial class DBSettings : Window
    {
        public DBSettings()
        {
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connector connector = new Connector();
                connector.CheckConnection(this.textBoxConnectionString.Text);
                Connector.ConnectionString = this.textBoxConnectionString.Text;
                MessageBox.Show("Успешно подключено!");
                this.Close();
            }
            catch (Exception)
            {
                var mbResult = MessageBox.Show("Произошла ошибка при подключении! Продолжить в любом случае?", "Ошибка!", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (mbResult == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.textBoxConnectionString.Text = Connector.ConnectionString;
        }
    }
}
