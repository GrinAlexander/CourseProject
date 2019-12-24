using System;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Data;
using System.Collections.Generic;
using System.Windows;

namespace CourseProject
{
    class Connector
    {
        public static string ConnectionString { get; set; } = "Data Source=.\\SQLEXPRESS;Initial Catalog=AutoParts;Integrated Security=True";

        public Connector() { }

        public void CheckConnection(string cs)
        {
            using (SqlConnection sqlConnection = new SqlConnection(cs))
            {
                sqlConnection.OpenAsync();
                sqlConnection.Close();
            }
        }

        public DataTable GetDataView(string tN)
        {
            string tableName = tN;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM {tableName}View";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.ExecuteNonQuery();
                DataTable datatable = new DataTable();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command))
                {
                    sqlDataAdapter.Fill(datatable);
                }
                sqlConnection.Close();
                return datatable;
            }
        }

        public List<string> GetListTables()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                List<string> tables = new List<string>();
                DataTable dt = sqlConnection.GetSchema("Tables");
                foreach (DataRow row in dt.Rows)
                {
                    string tablename = (string)row[2];
                    tables.Add(tablename);
                }
                sqlConnection.Close();
                return tables;
            }
        }

        public DataTable GetComboBoxData(string tN)
        {
            string tableName = tN;
            DataTable datatable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM {tableName}";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(datatable);
                }
                cmd.Dispose();
                sqlConnection.Close();
                return datatable;
            }
        }

        public void InsertIntoStorage(string adress, int capacity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {

                sqlConnection.Open();
                string query = $"INSERT INTO Склад VALUES (N'{adress}', {capacity})";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.ExecuteNonQuery();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command)) { }
                sqlConnection.Close();
            }
        }

        public void InsertIntoDetail(int id_s, string name, string prod, string category, float price)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                string query = $"INSERT INTO Деталь VALUES ({id_s},  N'{name}',  N'{prod}',  N'{category}', {price})";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.ExecuteNonQuery();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command)) { }
                sqlConnection.Close();
            }
        }

        public void InsertIntoDefect(int id_d, string description)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                string query = $"INSERT INTO Брак VALUES ({id_d}, N'{description}')";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.ExecuteNonQuery();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command)) { }
                sqlConnection.Close();
            }
        }

        public void InsertIntoOrder(int id_d, int count)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {

                sqlConnection.Open();
                string query = $"INSERT INTO Заказ (id_детали, количество) VALUES ({id_d}, {count})";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.ExecuteNonQuery();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command)) { }
                sqlConnection.Close();
            }
        }

        public void InsertIntoProvider(int id_o, string name)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                string query = $"INSERT INTO Поставщик VALUES ({id_o}, N'{name}')";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.ExecuteNonQuery();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command)) { }
                sqlConnection.Close();
            }
        }

        public void InsertIntoRequest(int id_d, int amount)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                string query = $"INSERT INTO Заявка (id_детали, количество) VALUES ({id_d}, {amount})";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.ExecuteNonQuery();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command)) { }
                sqlConnection.Close();
            }
        }

        public void InsertIntoSell(int id_o, DateTime? date)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                string query = $"INSERT INTO Продажа VALUES ({id_o}, N'{date}')";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.ExecuteNonQuery();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command)) { }
                sqlConnection.Close();
            }
        }
    }
}
