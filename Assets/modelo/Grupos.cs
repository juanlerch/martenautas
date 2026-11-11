using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grupos{
	public List<string> listado = new List<string>();
	public List<string> ids     = new List<string>();

}

[System.Serializable]
public class RetornoCrearGrupo{
	public int estado;
	public string mensaje;
}


[System.Serializable]
public class RetornoAgregarMiembro{
	public int estado;
	public int id_grupo;
	public string mensaje;

}
