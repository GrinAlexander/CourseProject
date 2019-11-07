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
        DataClassesDataContext dataClasses = new DataClassesDataContext(
            Properties.Settings.Default.ERBookConnectionString);

        public Tables()
        {
            InitializeComponent();
            FillComboBox();
            if (dataClasses.DatabaseExists())
            {
                this.comboBoxMain.SelectedIndex = 0;
            }
        }

        private void FillComboBox()
        {
            Connector connector = new Connector();
            List<string> tables = connector.GetListTables();
            this.comboBoxMain.ItemsSource = tables;
        }

        private void comboBoxMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tableName = (sender as ComboBox).SelectedItem as string;
            if (tableName.Equals("Students"))
            {
                this.dataGridMain.ItemsSource = dataClasses.Students;
            }
            else if (tableName.Equals("Teachers"))
            {
                this.dataGridMain.ItemsSource = dataClasses.Teachers;
            }
            else if (tableName.Equals("RegisterBook"))
            {
                this.dataGridMain.ItemsSource = dataClasses.RegisterBook;
            }
            labelTableName.Content = $"Выбрана таблица \"{tableName}\":";
        }

        private void buttonUpdateDB_Click(object sender, RoutedEventArgs e)
        {
            dataClasses.SubmitChanges();
        }
    }
}
