using Microsoft.Data.SqlClient;
using Dapper;

namespace TP07.Models;

public class BaseDeDatosTareas{
    public List<Tarea> LevantarTareas()
{
    List<Tarea> tareas = new List<Tarea>();
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "SELECT * FROM Tareas";
        tareas = connection.Query<Patente>(query).ToList();
    }
    return tareas;
}
public void AgregarTarea(Tarea tar)
{
    string query = "INSERT INTO Tareas (NombreTarea, Descripcion, IdTarea, Activo) VALUES (@pNombreTarea, @pDescripcion, @pIdTarea, @pActivo)";
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Execute(query, new { pNombreTarea = tar.NombreTarea, pDescripcion = tar.Descripcion, pIdTarea = tar.IdTarea, pActivo = tar.Activo });
    }
}


}