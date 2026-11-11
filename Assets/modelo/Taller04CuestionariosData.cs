using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Respuesta
{
	public string respuesta;
	public int correcta;

	public Respuesta set(string resp, int corr){
		respuesta = resp;
		correcta = corr;
		return this;
	}

}
[System.Serializable]
public class Pregunta
{
	public string enunciado;
	public List<Respuesta> respuestas;
}
[System.Serializable]
public class Taller04CuestionariosData
{
	public string titulo;
	public List<Pregunta> preguntas;
	public int puntoOK=100;
	public int puntoERR=-50;
}

