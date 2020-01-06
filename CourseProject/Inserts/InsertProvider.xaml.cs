using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseProject.Inserts
{
    /// <summary>
    /// Interaction logic for InsertProvider.xaml
    /// </summary>
    public partial class InsertProvider : Window
    {
        public InsertProvider()
        {
            InitializeComponent();
            FillComboBoxes();
        }

        private void buttonInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connector connector = new Connector();
                connector.InsertProvider(Convert.ToInt32(comboBoxOrder.SelectedValue), this.textBoxName.Text);
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
            DataTable dt = connector.GetTable("Заказ");

            comboBoxOrder.ItemsSource = dt.DefaultView;
            comboBoxOrder.DisplayMemberPath = "id";
            comboBoxOrder.SelectedValuePath = "id";
        }
    }
}
