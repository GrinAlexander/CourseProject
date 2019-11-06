using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Tables.xaml
    /// </summary>
    public partial class Tables : Window
    {
        public Tables()
        {
            InitializeComponent(); 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBox();
            comboBoxMain.SelectedIndex = 0;
        }

        private void FillComboBox()
        {
            Connector connector = new Connector();
            List<string> tables = connector.GetListTables();
            this.comboBoxMain.ItemsSource = tables;
        }

        private void FillDataGridByTableName(string tableName)
        {
            Connector connector = new Connector();
            this.dataGridMain.ItemsSource = connector.GetDataTable(tableName).DefaultView;
            labelTableName.Content = $"Выбрана таблица \"{tableName}\":";
        }

        private void comboBoxMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillDataGridByTableName((sender as ComboBox).SelectedItem as string);
        }
    }
}
