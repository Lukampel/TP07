using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP07.Models;

namespace TP07.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Perfil(string NombreUsuario, string contrasena)
{
    var UsuarioPerfil = BaseDeDatosUsuarios.LevantarUsuario(NombreUsuario);

    if (UsuarioPerfil != null && UsuarioPerfil.InicioSesion(contrasena))
    {
        HttpContext.Session.SetString("NombreUsuario", UsuarioPerfil.NombreUsuario);
        ViewBag.Usuario = UsuarioPerfil;
        return View("Perfil");
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
public IActionResult Registrar(string NombreUsuarioIngresado, string Contrasena){
    ViewBag.Mensaje = "";
    if(BaseDeDatosUsuarios.LevantarUsuario(NombreUsuarioIngresado) == null){
        BaseDeDatosUsuarios.AgregarUsuario(NombreUsuarioIngresado, Contrasena);
        ViewBag.Mensaje = "Usuario creado correctamente";
        return View("Tareas");
    } else {
        ViewBag.Mensaje = "El nombre ya está en uso";
        return RedirectToAction("Index");
    }
}
public IActionResult Registro()
{
    return View();
}

}
