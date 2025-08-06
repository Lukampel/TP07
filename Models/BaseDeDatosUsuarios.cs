using Microsoft.Data.SqlClient;
using Dapper;

namespace TP07.Models;

public static class BaseDeDatosUsuarios
{
   private static string _connectionString = @"Server=localhost;DataBase=TP07;Integrated Security=True;TrustServerCertificate=True;";

    public static Usuario LevantarUsuario(string nombreUsuario)
{
    Usuario miUsuario = null;
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
string query = @"SELECT 
                    NombreUsuario, 
                    Contrasena, 
                FROM Usuarios 
                WHERE NombreUsuario = @NombreUsuario";


       miUsuario = connection.QueryFirstOrDefault<Usuario>(
    query, new { NombreUsuario = nombreUsuario });

    }
    return miUsuario;
}

public static void AgregarUsuario(string NombreUsuarioIngresado, string Contrasena)
{
    string query = "INSERT INTO Usuarios (NombreUsuario, Contrasena) VALUES (@NombreUsuarioIngresado, @pContrasena)";
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Execute(query, new { pNombreUsuario = NombreUsuarioIngresado, pContrasena = Contrasena});
    }
}

}
