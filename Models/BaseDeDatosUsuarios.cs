using Microsoft.Data.SqlClient;
using Dapper;

namespace TP07.Models;

public class BaseDeDatosUsuarios
{
   private static string _connectionString = @"Server=localhost;DataBase=TP07PROG;Integrated Security=True;TrustServerCertificate=True;";

    public static Usuario LevantarUsuario(string nombreUsuario)
{
    Usuario miUsuario = null;
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
string query = @"SELECT 
                    Nombre, 
                    Contrasena 
                FROM Usuarios 
                WHERE Nombre = @pnombreUsuario";


       miUsuario = connection.QueryFirstOrDefault<Usuario>(
    query, new { pnombreUsuario = nombreUsuario });

    }
    return miUsuario;
}

public static void AgregarUsuario(string NombreUsuarioIngresado, string Contrasena)
{
    string query = "INSERT INTO Usuarios (Nombre, Contrasena) VALUES (@pNombreUsuarioIngresado, @pContrasena)";
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Execute(query, new { pNombreUsuarioIngresado = NombreUsuarioIngresado, pContrasena = Contrasena});
    }
}

}
