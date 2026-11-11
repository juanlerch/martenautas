using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AvanceActividad  {

    public string nombre;
    public List<int> puntajes = new List<int>(); //el puntaje por nivel (index 0 = nivel 1)

    public List<int> puntajesMaximo = new List<int>();

    public void actualizar(int nivel, int puntaje,int puntajeMaximo) {
        if (nivel > puntajes.Count)
        {
            int dif = (nivel - puntajes.Count );
            for (int t = 0; t < dif; t++) { 
                puntajes.Add(0);
                puntajesMaximo.Add(0);
            }
        }
            puntajes[nivel-1] = puntaje;
            puntajesMaximo[nivel - 1] = puntajeMaximo;
    }

    public int puntaje(int nivel) {
		if (nivel > puntajes.Count) {
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
