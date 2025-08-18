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
        return RedirectToAction("Index", "Usuarios"); 
    }

    Usuario usu = Objeto.StringToObject<Usuario>(userString);
    ViewBag.ListaTareas = BaseDeDatosTareas.LevantarTareas(usu.Nombre);
    ViewBag.NumeroTarea = 0;

    return View("ListaTareas", "Tareas");
}

public IActionResult CrearTarea(Tarea nuevaTarea) 
{
    

    BaseDeDatosTareas.AgregarTarea(nuevaTarea);

  return RedirectToAction("ObtenerListaTareas");
}


public IActionResult EditarTarea(string NombreTarea, string Descripcion, bool Activa, bool TareaFinalizada)
{
    Tarea tareaParaEditar = new Tarea
    {
        NombreTarea = NombreTarea,
        Descripcion = Descripcion,
        Activa = Activa,
        TareaFinalizada = TareaFinalizada
    };

   
    BaseDeDatosTareas.EditarTarea(tareaParaEditar);

    return RedirectToAction("ObtenerListaTareas");
}

public IActionResult EliminarTarea(Tarea tarea){
    
    Usuario usu = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));

    ViewBag.ListaTareas = BaseDeDatosTareas.LevantarTareas(usu.Nombre);
    BaseDeDatosTareas.EliminarTarea(tarea);

    return RedirectToAction("ObtenerListaTareas");
}
public IActionResult FinalizarTarea(int Id){

    Usuario usu = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));
    ViewBag.ListaTareas = BaseDeDatosTareas.LevantarTareas(usu.Nombre);

    if(ViewBag.ListaTareas[Id].Activa)
    ViewBag.ListaTareas[Id].Activa = false;

    return RedirectToAction("ObtenerListaTareas");
}

public IActionResult NuevaTarea(){
    return View ("NuevaTarea");
}
public IActionResult ListaTareas()
{
    return View("ListaTareas");
}
}
