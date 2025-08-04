using Microsoft.Data.SqlClient;
using Dapper;

namespace TP06.Models;

public static class BaseDeDatos
{
   private static string _connectionString = 
    @"Server=localhost;DataBase=TP07;Integrated Security=True;TrustServerCertificate=True;";
}
