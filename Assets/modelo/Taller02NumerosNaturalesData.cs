using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Taller02NumerosNaturalesData
{ 

	public string operacion;

	public List<string> resultados = new List<string>(); 
	public List<string> opciones = new List<string>();	

	public int puntoOK=100;
	public int puntoERR=-30;

    public void addResultados(int[] resultadoa) {
        foreach (int r in  resultadoa) { 
            resultados.Add("" + r);
        }

    }

    public void addOpciones(int[] opcionesa)
    {
        foreach (int o in opcionesa)
        {
            opciones.Add("" + o);
        }

    }



}
