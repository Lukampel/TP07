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


public IActionResult EditarTarea(int id)
{
    Usuario usu = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));
    Tarea tarea = BaseDeDatosTareas.LevantarTareas(usu.Nombre).FirstOrDefault(t => t.IdTarea == id);

    return View("EditarTarea", tarea);
}

[HttpPost]
public IActionResult EditarTarea(Tarea tareaParaEditar)
{

    if (tareaParaEditar.FechaVencimiento < new DateTime(1753, 1, 1) || tareaParaEditar.FechaVencimiento > new DateTime(9999, 12, 31))
    {
        ModelState.AddModelError("FechaVencimiento", "La fecha debe estar entre 01/01/1753 y 31/12/9999.");
        return View("EditarTarea", tareaParaEditar);
    }


    if (!ModelState.IsValid)
        return View("EditarTarea", tareaParaEditar);


    BaseDeDatosTareas.EditarTarea(tareaParaEditar);


    return RedirectToAction("ObtenerListaTareas");
}

public IActionResult EliminarTarea(int id)
{
    Usuario usu = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));
    var tarea = BaseDeDatosTareas.LevantarTareas(usu.Nombre).FirstOrDefault(t => t.IdTarea == id);
    if (tarea != null)
    {
        BaseDeDatosTareas.EliminarTarea(tarea);
    }
    return RedirectToAction("ObtenerListaTareas");
}

public IActionResult FinalizarTarea(int id)
{
    Usuario usu = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));
    var tarea = BaseDeDatosTareas.LevantarTareas(usu.Nombre).FirstOrDefault(t => t.IdTarea == id);
    if (tarea != null && tarea.Activa)
    {
        tarea.Activa = false;
        tarea.TareaFinalizada = true;
        BaseDeDatosTareas.EditarTarea(tarea);
    }
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
