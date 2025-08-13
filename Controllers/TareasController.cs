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
    var userString = HttpContext.Session.GetString("user");
    if (string.IsNullOrEmpty(userString))
    {
        return RedirectToAction("Index", "Usuarios"); // o a donde corresponda
    }

    Usuario usu = Objeto.StringToObject<Usuario>(userString);
    ViewBag.ListaTareas = BaseDeDatosTareas.LevantarTareas(usu.Nombre);
    ViewBag.NumeroTarea = 0;

    return View("ListaTareas", "Tareas");
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
    
    Usuario usu = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));

    ViewBag.ListaTareas = BaseDeDatosTareas.LevantarTareas(usu.Nombre);
    ViewBag.ListaTareas[Id].Activo = false;

    return RedirectToAction("ObtenerListaTareas");
}
public IActionResult FinalizarTarea(int Id){

    Usuario usu = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));
    ViewBag.ListaTareas = BaseDeDatosTareas.LevantarTareas(usu.Nombre);

    if(ViewBag.ListaTareas[Id].Activo)
    ViewBag.ListaTareas[Id].Activo = false;

    return RedirectToAction("ObtenerListaTareas");
}


public IActionResult ListaTareas()
{
    return View("ListaTareas");
}
}
