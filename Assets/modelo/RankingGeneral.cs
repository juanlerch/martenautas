using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RankingGeneral
{
	public int posicionUsuario;
	public int cantidadPaginas;
	public int posicionInicial;
	public List<Ranking> ranking;
}

[System.Serializable]
public class Ranking
{
	public int posicion;
    public string nombreUsuario;
    public string puntaje;
	public int avatar;
    public string nivel;
	public List<AvanceLogro> logros;

}

