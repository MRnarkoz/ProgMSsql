using System;
using System.Data;
using System.Data.SqlClient;

namespace ProgSQLiteZakaz
{
    class Program
    {
        static void Main(string[] args)
        {
            DB();
            Console.ReadKey();
        }
        static void DB()
        {
            string connString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Sales; Integrated Security = True"; 
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("Select * from Sales.Orders", connection))
                {
                    //SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        if (dataReader.HasRows)
                        {
                            string columnNameCustomerID = dataReader.GetName(0);
                            string columnNameOrderID = dataReader.GetName(1);
                            string columnNameOrderDate = dataReader.GetName(2);
                            string columnNameFilledDate = dataReader.GetName(3);
                            string columnNameStatus = dataReader.GetName(4);
                            string columnNameAmount = dataReader.GetName(5);

                            Console.WriteLine($"{columnNameCustomerID}\t{columnNameOrderID} \t{columnNameOrderDate}\t\t{columnNameFilledDate}\t\t{columnNameStatus}\t{columnNameAmount}");

                            while (dataReader.Read())
                            {
                                var CustomerID = dataReader.GetValue(0);
                                var OrderID = dataReader.GetValue(1);
                                var OrderDate = dataReader.GetValue(2);
                                var FilledDate = dataReader.GetValue(3);
                                var Status = dataReader.GetValue(4);
                                var Amount = dataReader.GetValue(5);

                                Console.WriteLine($"{CustomerID}\t\t{OrderID}\t\t{OrderDate}\t{FilledDate}\t{Status}\t{Amount}");
                            }
                        }
                    }
                    }
                    //закрвываем соединение
                    connection.Close();
                    connection.Dispose();
                    Console.WriteLine();
                }
            }
        }

    }

