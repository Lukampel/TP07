using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP07.Models;
using Microsoft.Data.SqlClient;
using Dapper;


namespace TP07.Controllers;

public class TareasController : Controller
{
    public IActionResult ObtenerListaTareas()
    {
        ViewBag.ListaTareas = BaseDeDatosTareas.LevantarTareas();
        ViewBag.NumeroTarea = 0;
        return View("ListaTareas");
    }
 public IActionResult CrearTarea(string NombreTarea, string Descripcion, bool Activo, bool TareaFinalizada) 
{
    Tarea nuevaTarea = new Tarea
    {
        NombreTarea = NombreTarea,
        Descripcion = Descripcion,
        Activo = Activo,
        TareaFinalizada = TareaFinalizada
    };

    BaseDeDatosTareas.AgregarTarea(nuevaTarea);

  return RedirectToAction("ObtenerListaTareas");
}


public IActionResult EditarTarea(string NombreTarea, string Descripcion, bool Activo, bool TareaFinalizada)
{
    Tarea tareaParaEditar = new Tarea
    {
        NombreTarea = NombreTarea,
        Descripcion = Descripcion,
        Activo = Activo,
        TareaFinalizada = TareaFinalizada
    };

   
    BaseDeDatosTareas.EditarTarea(tareaParaEditar);

    return RedirectToAction("ObtenerListaTareas");
}

public IActionResult EliminarTarea(int Id){
    
    ViewBag.ListaTareas = BaseDeDatosTareas.LevantarTareas();
    ViewBag.ListaTareas[Id].Activo = false;

    return RedirectToAction("ObtenerListaTareas");
}
}
