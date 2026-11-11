using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Registro{
	public string usuario;
	public string password;
	public string email;

}

[System.Serializable]
public class RetornoRegistro {
	public int estado;
	public string usuario;
	public string token;
	public string mensaje;

}

