using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoADO
{
    internal class Program
    {
        const string connectionString = "Server=.\\SQLEXPRESS;Database=Demo;User Id=sa;Password=123456;TrustServerCertificate=True";
        static void Main(string[] args)
        {
            InserBook("C# 9.0 in a Nutshell", 900);
            Getbooks();
        }
        public static void Getbooks()
        {
            SqlConnection con = new SqlConnection(connectionString);

            string query = "SELECT * FROM Books";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Books");

            DataTable tblBooks = ds.Tables["Books"];
            foreach (DataRow dr in tblBooks.Rows)
            {
                Console.WriteLine($"ID={dr[0]}, Title={dr[1]}, Price={dr[2]}");
            }
        }
        public static void InserBook(string title, int price)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string query = "INSERT INTO Books (Title, Price) VALUES (@title, @price)";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            // truyen tham so
            cmd.Parameters.Add(new SqlParameter("title", title));
            cmd.Parameters.Add(new SqlParameter("price", price));
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
