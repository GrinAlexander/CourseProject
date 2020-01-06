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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CourseProject.Inserts;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Connector connector = new Connector();
        private int id_storage = 0;
        private int id_detail = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonAddStorage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InsertStorage insertStorage = new InsertStorage();
                insertStorage.ShowDialog();
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dataGridStorage.ItemsSource = connector.GetTable("СкладView").DefaultView;
                dataGridDetails.ItemsSource = connector.GetDataTableByQuery($"SELECT TOP 1 * FROM ДетальНаСкладеView").DefaultView;
                SetVisibility();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridStorage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridStorage.SelectedItem != null)
            {
                buttonRemoveStorage.IsEnabled = true;
                DataRowView row = dataGridStorage.SelectedItem as DataRowView;
                id_storage = (int)row.Row[0];
                dataGridDetails.ItemsSource = connector.GetDataTableByQuery($"SELECT * FROM ДетальНаСкладеView WHERE id = {id_storage}").DefaultView;
                SetVisibility();
            }
            else
            {
                buttonRemoveStorage.IsEnabled = false;
            }
        }

        private void Refresh()
        {
            try
            {
                dataGridStorage.ItemsSource = connector.GetTable("СкладView").DefaultView;
                dataGridDetails.ItemsSource = connector.GetDataTableByQuery($"SELECT * FROM ДетальНаСкладеView WHERE id = {id_storage}").DefaultView;
                SetVisibility();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetVisibility()
        {
            dataGridStorage.Columns[0].Visibility = Visibility.Collapsed;
            dataGridDetails.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void buttonAddDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InsertDetail insertDetail = new InsertDetail();
                insertDetail.ShowDialog();
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonRemoveDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connector.Delete("Деталь", "id", id_detail);
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonRemoveStorage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connector.Delete("Склад", "id", id_storage);
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridDetails.SelectedItem != null)
            {
                buttonRemoveDetail.IsEnabled = true;
                DataRowView row = dataGridDetails.SelectedItem as DataRowView;
                id_detail = (int)row.Row[0];
            }
            else
            {
                buttonRemoveDetail.IsEnabled = false;
            }
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                dataGridSearch.ItemsSource = connector.GetDataTableByQuery( //Авто.марка, Авто.модель, Деталь.артикул, Деталь.категория, Деталь.название, Деталь.производитель
                    $"SELECT * FROM SearchView WHERE марка LIKE '%{textBoxSearch.Text}%' OR модель LIKE '%{textBoxSearch.Text}%' OR артикул LIKE '%{textBoxSearch.Text}%' OR категория LIKE '%{textBoxSearch.Text}%' OR название LIKE '%{textBoxSearch.Text}%' OR производитель LIKE '%{textBoxSearch.Text}%'").DefaultView;
                dataGridSearch.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
