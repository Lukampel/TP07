using Microsoft.Data.SqlClient;
using Dapper;

namespace TP07.Models;

public class BaseDeDatosTareas
{
    private static string _connectionString = @"Server=localhost\SQLEXPRESS;DataBase=TP07PROG;Integrated Security=True;TrustServerCertificate=True;";

 public static List<Tarea> LevantarTareas(string nombreUsuario)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = @"
            SELECT t.IdTarea, t.Descripcion, t.FechaVencimiento , t.NombreTarea, t.Activa, t.TareaFinalizada
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
        string query = "INSERT INTO Tareas (NombreTarea, Descripcion, Activa, TareaFinalizada, FechaVencimiento) VALUES (@pNombreTarea, @pDescripcion, @pActiva, @pTareaFinalizada, @pFechaVencimiento)";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { pNombreTarea = tar.NombreTarea, pDescripcion = tar.Descripcion, pActiva = tar.Activa,pTareaFinalizada = tar.TareaFinalizada, pFechaVencimiento = tar.FechaVencimiento});
        }
    }
    public static void EditarTarea(Tarea tar) 
    {
        string query = "UPDATE Tareas SET NombreTarea = @pNombreTarea, Descripcion = @pDescripcion, Activa = @pActiva, TareaFinalizada = @pTareaFinalizada, FechaVencimiento = @pFechaVencimiento WHERE IdTarea = @pIdTarea";
         using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { pNombreTarea = tar.NombreTarea, pDescripcion = tar.Descripcion, pActiva = tar.Activa, pTareaFinalizada = tar.TareaFinalizada, pFechaVencimiento = tar.FechaVencimiento, pIdTarea = tar.IdTarea });
        }
    }
    public static void EliminarTarea(Tarea tar) 
    {
    string query = "UPDATE Tareas SET Activa = 0 WHERE IdTarea = @pIdTarea";

    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Execute(query, new { pIdTarea = tar.IdTarea });
   }

    }
   public static void TerminarTarea(Tarea tar) 
{
    string query = "UPDATE Tareas SET Activa = 0 WHERE IdTarea = @pIdTarea";

    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Execute(query, new { pIdTarea = tar.IdTarea });
   }
   }


}
