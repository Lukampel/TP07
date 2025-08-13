using Microsoft.Data.SqlClient;
using Dapper;

namespace TP07.Models;

public class BaseDeDatosTareas
{
    private static string _connectionString = @"Server=localhost;DataBase=TP07PROG;Integrated Security=True;TrustServerCertificate=True;";

 public static List<Tarea> LevantarTareas(string nombreUsuario)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = @"
            SELECT t.IdTarea, t.Descripcion, t.FechaVencimiento
            FROM Tareas t
            INNER JOIN UsuarioXTareas ut ON ut.IdTarea = t.IdTarea 
            INNER JOIN Usuarios u ON ut.IdUsuario = u.IdUsuario
            WHERE u.Nombre = @pNombreUsuario";

        var tareas = connection.Query<Tarea>(
            query, new { pNombreUsuario = nombreUsuario }).ToList();

        return tareas;
    }
}

    public static void AgregarTarea(Tarea tar)
    {
        string query = "INSERT INTO Tareas (NombreTarea, Descripcion, Activo, TareaFinalizada) VALUES (@pNombreTarea, @pDescripcion, @pActivo, @pTareaFinalizada)";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { pNombreTarea = tar.NombreTarea, pDescripcion = tar.Descripcion, pActivo = tar.Activo,pTareaFinalizada = tar.TareaFinalizada});
        }
    }
    public static void EditarTarea(Tarea tar) 
    {
        string query = "UPDATE Tareas SET (NombreTarea, Descripcion, Activo, TareaFinalizada) VALUES (@pNombreTarea, @pDescripcion, @pActivo, @pTareaFinalizada)";
         using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { pNombreTarea = tar.NombreTarea, pDescripcion = tar.Descripcion, pActivo = tar.Activo, pTareaFinalizada = tar.TareaFinalizada });
        }
    }
    public static void EliminarTarea(Tarea tar) 
    {
    string query = "UPDATE Tareas SET Activo = 0 WHERE IdTarea = @IdTarea";

    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Execute(query, new { tar.IdTarea });
   }

    }
   public static void TerminarTarea(Tarea tar) 
{
    string query = "UPDATE Tareas SET TareaFinalizada = 0 WHERE IdTarea = @IdTarea";

    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Execute(query, new { tar.IdTarea });
   }
   }


}
