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
            
            CurrentDB.ConnectionString = textBoxConnectionString.Text;
            Connector connector = new Connector();
            var result = Task.Run(connector.CheckConnectionAsync);

            //bool result = connector.CheckConnection();
            
            if (result.Result)
            {
                MessageBox.Show("Успешно подключено!");
                this.Close();
            }
            else
            {
                var mbResult = MessageBox.Show("Произошла ошибка при подключении! Продолжить в любом случае?", "Ошибка!", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (mbResult == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            
        }
    }
}
