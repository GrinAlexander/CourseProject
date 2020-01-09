using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
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
using Microsoft.Reporting.WinForms;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Connector connector = new Connector();
        private int id_storage = 0;
        private int id_detail = 0;
        private int id_order = 0;
        private DataTable orderTable;

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
                RefreshFirst();
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
                dataGridDetailsOrder.ItemsSource = connector.GetTable("ЗаказView").DefaultView;
                dataGridDetails2.ItemsSource = connector.GetDataTableByQuery($"SELECT TOP 1 * FROM ЗаказДеталиView").DefaultView;
                NewOrderDataGrid();
                SetVisibilityFirst();
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
                SetVisibilityFirst();
            }
            else
            {
                buttonRemoveStorage.IsEnabled = false;
            }
        }

        private void RefreshFirst()
        {
            try
            {
                dataGridStorage.ItemsSource = connector.GetTable("СкладView").DefaultView;
                dataGridDetails.ItemsSource = connector.GetDataTableByQuery($"SELECT * FROM ДетальНаСкладеView WHERE id = {id_storage}").DefaultView;
                SetVisibilityFirst();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefreshSecond()
        {
            try
            {
                dataGridDetailsOrder.ItemsSource = connector.GetTable("ЗаказView").DefaultView;
                dataGridDetails2.ItemsSource = connector.GetDataTableByQuery($"SELECT * FROM ЗаказДеталиView WHERE id = {id_order}").DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetVisibilityFirst()
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
                RefreshFirst();
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
                RefreshFirst();
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
                RefreshFirst();
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

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                dataGridSearch.ItemsSource = connector.GetDataTableByQuery(
                    $"SELECT * FROM SearchView WHERE марка LIKE '%{textBoxSearch.Text}%' OR модель LIKE '%{textBoxSearch.Text}%' OR артикул LIKE '%{textBoxSearch.Text}%' OR категория LIKE '%{textBoxSearch.Text}%' OR название LIKE '%{textBoxSearch.Text}%' OR производитель LIKE '%{textBoxSearch.Text}%'").DefaultView;
                dataGridSearch.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonBill_Click(object sender, RoutedEventArgs e)
        {
            string html = GetHtmlBody();
            SendAutomatedEmail(html, "androidgryn777@gmail.com");
            AddOrderInDB();
        }

        public string GetHtmlBody()
        {
            string messageBody = "<font>The following are the records: </font><br><br>";
            string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
            string htmlTableEnd = "</table>";
            string htmlHeaderRowStart = "<tr style =\"background-color:#6FA1D2; color:#ffffff;\">";
            string htmlHeaderRowEnd = "</tr>";
            string htmlTrStart = "<tr style =\"color:#555555;\">";
            string htmlTrEnd = "</tr>";
            string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
            string htmlTdEnd = "</td>";

            messageBody += htmlTableStart;
            messageBody += htmlHeaderRowStart;
            messageBody += htmlTdStart + "Артикул" + htmlTdEnd;
            messageBody += htmlTdStart + "Количество" + htmlTdEnd;
            messageBody += htmlTdStart + "Стоимость" + htmlTdEnd;
            messageBody += htmlHeaderRowEnd;

            foreach (DataRow Row in orderTable.Rows)
            {
                messageBody += htmlTrStart;
                messageBody += htmlTdStart + Row["Артикул"] + htmlTdEnd;
                messageBody += htmlTdStart + Row["Количество"] + htmlTdEnd;
                messageBody += htmlTdStart + Row["Стоимость"] + htmlTdEnd;
                messageBody += htmlTrEnd;
            }
            messageBody += htmlTableEnd;
            return messageBody;
        }

        public static void SendAutomatedEmail(string htmlString, string recipient = "androidgryn777@gmail.com")
        {
            MailMessage message = new MailMessage("elirhard@gmail.com", recipient)
            {
                IsBodyHtml = true,
                Body = htmlString,
                Subject = "Ваш заказ в магазине \"AutoParts\":"
            };

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("elirhard@gmail.com", "01234569Elir")
            };
            client.Send(message);
            MessageBox.Show("Сообщение отправлено!");
        }

        private void buttonInOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridSearch.SelectedItem != null)
            {
                DataRowView row = dataGridSearch.SelectedItem as DataRowView;
                AddInOrder(row);
                dataGridOrder.ItemsSource = orderTable.DefaultView;
            }
        }

        private void AddInOrder(DataRowView rowView)
        {
            orderTable.Rows[orderTable.Rows.Count - 1].Delete();
            int? rowIndex = IsOrdered(rowView.Row[3].ToString());
            if (rowIndex != null)
            {
                int count = (int)orderTable.Rows[rowIndex.Value]["Количество"];
                orderTable.Rows[rowIndex.Value]["Количество"] = ++count;
                orderTable.Rows[rowIndex.Value]["Стоимость"] = count * double.Parse(rowView[7].ToString());
                CalcTotalRow();
                return;
            }
            DataRow row = orderTable.NewRow();
            row["Артикул"] = rowView[3].ToString();
            row["Количество"] = 1;
            row["Стоимость"] = rowView[7].ToString();
            orderTable.Rows.Add(row);
            CalcTotalRow();
        }

        private int? IsOrdered(string articul)
        {
            for (int i = 0; i < orderTable.Rows.Count; i++)
            {
                if (orderTable.Rows[i]["Артикул"].ToString().ToLower() == articul.ToLower())
                {
                    return i;
                }
            }
            return null;
        }

        private void CalcTotalRow()
        {
            int count = 0;
            double price = 0;
            foreach (DataRow row in orderTable.Rows)
            {
                count += (int)row["Количество"];
                price += double.Parse(row["Стоимость"].ToString());
            }
            DataRow newRow = orderTable.NewRow();
            newRow["Артикул"] = "Итого";
            newRow["Количество"] = count;
            newRow["Стоимость"] = price;
            orderTable.Rows.Add(newRow);
        }

        private void NewOrderDataGrid()
        {
            orderTable = new DataTable();
            orderTable.Columns.Add("Артикул", typeof(string));
            orderTable.Columns.Add("Количество", typeof(int));
            orderTable.Columns.Add("Стоимость", typeof(double));
            DataRow row = orderTable.NewRow();
            row["Артикул"] = "Итого";
            row["Количество"] = 0;
            row["Стоимость"] = 0;
            orderTable.Rows.Add(row);
            dataGridOrder.ItemsSource = orderTable.DefaultView;
        }

        private void buttonClearOrder_Click(object sender, RoutedEventArgs e)
        {
            NewOrderDataGrid();
        }

        private void buttonAddOrder_Click(object sender, RoutedEventArgs e)
        {
            InsertOrder insertOrder = new InsertOrder();
            insertOrder.ShowDialog();
            RefreshSecond();
        }

        private void buttonRemoveOrder_Click(object sender, RoutedEventArgs e)
        {

            connector.Delete("Заказ", "id", id_order);
            RefreshSecond();
        }

        private void dataGridDetailsOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridDetailsOrder.SelectedItem != null)
            {
                buttonRemoveOrder.IsEnabled = true;
                DataRowView row = dataGridDetailsOrder.SelectedItem as DataRowView;
                id_order = (int)row.Row[0];
                dataGridDetails2.ItemsSource = connector.GetDataTableByQuery($"SELECT * FROM ЗаказДеталиView WHERE id = {id_order}").DefaultView;
            }
            else
            {
                buttonRemoveOrder.IsEnabled = false;
            }
        }

        private void buttonAddDetail2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InsertDetail insertDetail = new InsertDetail();
                insertDetail.ShowDialog();
                RefreshSecond();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonRemoveDetail2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connector.Delete("Деталь", "id", id_detail);
                RefreshSecond();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridDetails2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridDetails2.SelectedItem != null)
            {
                buttonRemoveDetail2.IsEnabled = true;
                DataRowView row = dataGridDetails2.SelectedItem as DataRowView;
                id_detail = (int)row.Row[0];
            }
            else
            {
                buttonRemoveDetail2.IsEnabled = false;
            }
        }

        private void FillReportViewer()
        {
            string reportName = "";
            string dataSourceName = "AutoPartsDataSet";
            string tableName = "";
            switch (comboBoxReports.SelectedIndex)
            {
                case 0:
                    {
                        reportName = "ReportPriceList";
                        tableName = "PriceListView";
                        break;
                    }
                case 1:
                    {
                        reportName = "ReportSalesInYear";
                        tableName = "SalesInYearView";
                        break;
                    }
                case 2:
                    {
                        reportName = "ReportTop3Sales";
                        tableName = "TopDetailsView";
                        break;
                    }
                case 3:
                    {
                        reportName = "ReportStoragesState";
                        tableName = "СостояниеСкладовView";
                        break;
                    }
                default:
                    return;
            }
            Connector connector = new Connector();
            ReportDataSource reportDataSource = new ReportDataSource(dataSourceName, connector.GetTable(tableName));
            reportViewerMain.LocalReport.DataSources.Add(reportDataSource);
            reportViewerMain.LocalReport.ReportEmbeddedResource = $"CourseProject.Reports.{reportName}.rdlc";
            reportViewerMain.RefreshReport();
        }

        private void buttonShowReport_Click(object sender, RoutedEventArgs e)
        {
            reportViewerMain.Reset();
            FillReportViewer();
        }

        private void buttonManual_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Manual.chm");
        }

        private void AddOrderInDB()
        {
            for (int i = 0; i < orderTable.Rows.Count - 1; i++)
            {
                string articul = orderTable.Rows[i]["Артикул"].ToString();
                int count = (int)orderTable.Rows[i]["Количество"];
                int id = connector.GetId($"SELECT TOP 1 id FROM Деталь WHERE артикул = '{articul}'");
                connector.InsertRequest(id, count);
            }
        }
    }
}
