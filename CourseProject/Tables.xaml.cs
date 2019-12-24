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
using System.Data.Linq.Mapping;

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
            FillComboBox();
            this.comboBoxMain.SelectedIndex = 0;
        }

        private void FillComboBox()
        {
            try
            {
                Connector connector = new Connector();
                List<string> tables = connector.GetListTables();
                tables.Remove("sysdiagrams");
                tables.RemoveAll(tn => (tn.Contains("View")));
                this.comboBoxMain.ItemsSource = tables;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void comboBoxMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string tableName = (sender as ComboBox).SelectedItem as string;
                Connector connector = new Connector();
                this.dataGridMain.ItemsSource = connector.GetDataView(tableName).DefaultView;
                labelTableName.Content = $"Выбрана таблица \"{tableName}\":";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
