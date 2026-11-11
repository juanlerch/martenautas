using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Login{
	public string usuario;
	public string password;

}

[System.Serializable]
public class RetornoLogin {
	public int estado;
	public string usuario;
	public string token;
	public int idActualizacion;
	public AvanceJugador avance;
	public string mensaje;


}

