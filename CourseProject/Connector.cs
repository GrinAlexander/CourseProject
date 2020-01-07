using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace CourseProject
{
    class Connector
    {
        public static string ConnectionString { get; set; } = Properties.Settings.Default.ConnectionString;

        public Connector() { }

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

        public void InsertStorage(string adress, int capacity)
        {
            string query = $"INSERT INTO Склад VALUES (N'{adress}', {capacity})";
            Insert(query);
        }

        public void InsertDetail(int id_s, string name, string articul, string prod, string category, float price)
        {
            string query = $"INSERT INTO Деталь VALUES ({id_s},  N'{name}', N'{articul}',  N'{prod}',  N'{category}', {price.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)})";
            Insert(query);
        }

        public void InsertDefect(int id_d, string description)
        {
            string query = $"INSERT INTO Брак VALUES ({id_d}, N'{description}')";
            Insert(query);
        }

        public void InsertOrder(int id_d, int count)
        {
            string query = $"INSERT INTO Заказ (id_детали, количество) VALUES ({id_d}, {count})";
            Insert(query);
        }

        public void InsertProvider(int id_o, string name)
        {
            string query = $"INSERT INTO Поставщик VALUES ({id_o}, N'{name}')";
            Insert(query);
        }

        public void InsertRequest(int id_d, int amount)
        {
            string query = $"INSERT INTO Заявка (id_детали, количество) VALUES ({id_d}, {amount})";
            Insert(query);
        }

        public void InsertSell(int id_o, DateTime? date)
        {
            string query = $"INSERT INTO Продажа VALUES ({id_o}, N'{date}')";
            Insert(query);
        }

        public void InsertAuto(string name, string model, string type, int year, float amount)
        {
            string query = $"INSERT INTO Авто VALUES ('{name}', '{model}', '{type}', {year}, {amount})";
            Insert(query);
        }

        private void Insert(string query)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.ExecuteNonQuery();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command)) { }
                sqlConnection.Close();
            }
        }

        public void Delete(string tN, string fN, int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                string query = $"DELETE FROM {tN} WHERE {fN} = {id}";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.ExecuteNonQuery();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command)) { }
                sqlConnection.Close();
            }
        }

        public DataTable GetDataTableByQuery(string query)
        {
            DataTable datatable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(datatable);
                cmd.Dispose();
                sqlConnection.Close();
                return datatable;
            }
        }

        public DataTable GetTable(string tN)
        {
            string tableName = tN;
            string query = $"SELECT * FROM {tableName}";
            return GetDataTableByQuery(query);
        }

        public List<string> GetListOfColumns(string tN)
        {
            string tableName = tN;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                string query = $"SELECT * FROM {tableName}View";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.ExecuteNonQuery();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable datatable = new DataTable();
                sqlDataAdapter.Fill(datatable);

                List<string> columns = new List<string>();
                foreach (DataColumn column in datatable.Columns)
                {
                    columns.Add(column.ColumnName);
                }
                sqlConnection.Close();
                return columns;
            }
        }

        public int GetId(string query)
        {
            DataTable datatable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(datatable);
                cmd.Dispose();
                sqlConnection.Close();
            }
            return Convert.ToInt32(datatable.Rows[0][0]);
        }
    }
}
