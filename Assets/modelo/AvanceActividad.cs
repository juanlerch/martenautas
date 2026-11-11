using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AvanceActividad  {

    public string nombre;
    public List<int> puntajes = new List<int>(); //el puntaje por nivel (index 0 = nivel 1)

    public List<int> puntajesMaximo = new List<int>();

	public List<AvanceEstadistica> estadisticas = new List<AvanceEstadistica>();

	 

	public AvanceEstadistica estadistica(int nivel) {
		if(estadisticas==null) estadisticas = new List<AvanceEstadistica>();
		if (nivel > estadisticas.Count)
		{
			int dif = (nivel - estadisticas .Count );
			for (int t = 0; t < dif; t++) { 
				estadisticas.Add(new AvanceEstadistica());
			}
		}
		return  estadisticas [nivel-1];
	}



    public void actualizar(int nivel, int puntaje,int puntajeMaximo) {
        if (nivel > puntajes.Count)
        {
            int dif = (nivel - puntajes.Count );
            for (int t = 0; t < dif; t++) { 
                puntajes.Add(0);
                puntajesMaximo.Add(0);
            }
        }

		if(puntaje> puntajes[nivel-1])  puntajes[nivel-1] = puntaje;
		if(puntajeMaximo> puntajesMaximo[nivel-1])  puntajesMaximo[nivel - 1] = puntajeMaximo;

    }

    public int puntaje(int nivel) {
		if (nivel > puntajes.Count || nivel == 0) {
			return 0;
		} else {
			return puntajes [nivel - 1];
		}
    }

    public int puntajeMaximo(int nivel)
    {
        if (nivel > puntajesMaximo.Count)
        {
            return 0;
        }
        else {
            return puntajesMaximo[nivel - 1];
        }
    }


}
