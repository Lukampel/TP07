public class Usuario
{
    public string NombreUsuario { get; set; }
    public string Contrasena { get; set; }
  

public bool InicioSesion(string ContrasenaIngresada)
{
    bool iguales = Contrasena == ContrasenaIngresada;
    return iguales;
}

public bool ElUsuarioExiste(string NombreUsuarioIngresado){
    return NombreUsuario == NombreUsuarioIngresado;
}

}
