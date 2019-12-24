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

namespace CourseProject.Inserts
{
    /// <summary>
    /// Логика взаимодействия для Inserts.xaml
    /// </summary>
    public partial class WindowInserts : Window
    {
        public WindowInserts()
        {
            InitializeComponent();
        }

        private void buttonDefect_Click(object sender, RoutedEventArgs e)
        {
            InsertDefect defect = new InsertDefect();
            defect.Show();
            this.Close();
        }

        private void buttonDetail_Click(object sender, RoutedEventArgs e)
        {
            InsertDetail detail = new InsertDetail();
            detail.Show();
            this.Close();
        }

        private void buttonOrder_Click(object sender, RoutedEventArgs e)
        {
            InsertOrder order = new InsertOrder();
            order.Show();
            this.Close();
        }

        private void buttonRequest_Click(object sender, RoutedEventArgs e)
        {
            InsertRequest insertRequest = new InsertRequest();
            insertRequest.Show();
            this.Close();
        }

        private void buttonProvider_Click(object sender, RoutedEventArgs e)
        {
            InsertProvider provider = new InsertProvider();
            provider.Show();
            this.Close();
        }

        private void buttonSale_Click(object sender, RoutedEventArgs e)
        {
            InsertSell sale = new InsertSell();
            sale.Show();
            this.Close();
        }

        private void buttonStore_Click(object sender, RoutedEventArgs e)
        {
            InsertStorage store = new InsertStorage();
            store.Show();
            this.Close();
        }
    }
}
