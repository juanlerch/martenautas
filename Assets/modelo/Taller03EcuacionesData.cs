using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Taller03EcuacionesData {

	public float resultado;
	public string planteoIzquierda;
	public string planteoDerecha;

	public string[][] validosIzquierda; 
	public string[][] validosDerecha; 
	public string[][] noValidos;
	public int puntoOK=250;
	public int puntoERR=0;

}
