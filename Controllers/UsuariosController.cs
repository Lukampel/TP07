using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP07.Models;

namespace TP07.Controllers;

public class UsuariosController : Controller
{
    private readonly ILogger<UsuariosController> _logger;

    public UsuariosController(ILogger<UsuariosController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if(HttpContext.Session.GetString("user") == null){
        return View("Index");
        } else{
            return View("ListaTareas");
        }
    }

    public IActionResult Perfil(string NombreUsuario, string contrasena)
{
    var UsuarioPerfil = BaseDeDatosUsuarios.LevantarUsuario(NombreUsuario);

    if (UsuarioPerfil != null && UsuarioPerfil.InicioSesion(contrasena))
    {
        HttpContext.Session.SetString("Nombre", UsuarioPerfil.Nombre);
        ViewBag.Usuario = UsuarioPerfil;
        return View("ListaTareas");
    }
    else
    {
        ViewBag.Mensaje = "Usuario o contraseña incorrectos";
        return View("Index");
    }
}

public IActionResult CerrarSesion()
{
    HttpContext.Session.Clear(); 
    return RedirectToAction("Index"); 
}


public IActionResult Registrar(string NombreUsuarioIngresado, string Contrasena)
{
    ViewBag.Mensaje = "";
    if(BaseDeDatosUsuarios.LevantarUsuario(NombreUsuarioIngresado) == null){
        BaseDeDatosUsuarios.AgregarUsuario(NombreUsuarioIngresado, Contrasena);
        ViewBag.Mensaje = "Usuario creado correctamente";
        return View("ListaTareas");
    } else {
        ViewBag.Mensaje = "El nombre ya está en uso";
        return RedirectToAction("Index");
    }
}
public IActionResult Registro()
{
    return View("Registro");
}
}
