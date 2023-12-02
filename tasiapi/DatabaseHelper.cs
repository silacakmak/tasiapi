using System;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using Npgsql.Internal;
using System.Data;

public class DatabaseHelper
{
    static void Main(string[] args)
    {
        Console.WriteLine("twatasdasd");
        //TestConnection();
        Console.ReadKey();

    }
    public void TestConnection()
    {
        using (NpgsqlConnection con = GetConnection())
        {
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                Console.WriteLine("connected");
            }
        }


    }
    private static NpgsqlConnection GetConnection()
    {

        string connectionString = "Host=localhost;Port=5432;Database=tasinmaz;Username=postgres;Password=1234";

        using (var connection = new NpgsqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("PostgreSQL veritabanına başarıyla bağlandınız.");

                // Veritabanı işlemlerini burada gerçekleştirebilirsiniz
                // Örneğin, sorguları çalıştırabilir veya veri alışverişi yapabilirsiniz

                //connection.Close();
                //Console.WriteLine("PostgreSQL bağlantısı başarılı bir şekilde kapatıldı.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }
        }
        return new NpgsqlConnection(@"Server=localhost;Port=5432;Username=postgres;Password=1234;Database=tasinmaz");
    }
}
//return new NpgsqlConnection(@"Server=localhost;Port=5432;Username=postgres;Password=1234;Database=tasinmaz");


