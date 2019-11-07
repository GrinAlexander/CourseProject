using System;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Data;
using System.Collections.Generic;
using System.Windows;

namespace CourseProject
{
    //Data Source=.\SQLEXPRESS;Initial Catalog=ERBook;Integrated Security=True
    class Connector
    {
        private string connectionString;

        public Connector()
        {
            this.connectionString = CurrentDB.ConnectionString;
        }

        public async Task<bool> CheckConnectionAsync()
        {
            bool result = false;
            await Task.Run(() =>
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.connectionString))
                {
                    try
                    {
                        sqlConnection.OpenAsync();
                        sqlConnection.Close();
                        result = true;
                    }
                    catch
                    {
                        result = false;
                    }
                }
            }
            );
            return result;
        }

        public DataTable GetDataTable(string tN)
        {
            string tableName = tN;
            DataTable datatable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(this.connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    string query = $"SELECT * FROM {tableName}";
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    command.ExecuteNonQuery();

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    datatable = new DataTable();
                    sqlDataAdapter.Fill(datatable);
                    sqlConnection.Close();
                    return datatable;
                }
                catch
                {
                    return datatable;
                }
            }
        }

        public List<string> GetListTables()
        {
            using (SqlConnection sqlConnection = new SqlConnection(this.connectionString))
            {
                try
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
                catch
                {
                    return new List<string>();
                }
            }
        }

        public void InsertIntoStudents(string group, string surname, string name, string patronymic)
        {
            using (SqlConnection sqlConnection = new SqlConnection(this.connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    string query = $"INSERT INTO Students VALUES (N'{group}', N'{surname}', N'{name}', N'{patronymic}')";
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    command.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command)) { }
                    sqlConnection.Close();
                }
                catch
                {

                }
                
            }
        }
    }
}
