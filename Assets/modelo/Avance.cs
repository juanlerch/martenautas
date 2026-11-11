using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Avance  {

	public string ultimoUsuario;

	public bool mantenerLogin=false;
	public bool internet=true;
	public bool sonido=true;

    public List<string>        nombreJugador = new List<string>();
    public List<AvanceJugador> avanceJugador= new List<AvanceJugador>();


    public void init()
    {

    }

	public void renombrar(string actual,string nuevo){
		int index = nombreJugador.IndexOf(actual);
		AvanceJugador avaj = avanceJugador[index];
		avaj.nombre = nuevo;
		nombreJugador [index] = nuevo;
		Principal.avance.ultimoUsuario = nuevo;
		Principal.user = nuevo;
	}

	public void sobrescribir (AvanceJugador avanceJugador1){
		if (nombreJugador.Contains (avanceJugador1.nombre)) {
			int index = nombreJugador.IndexOf (avanceJugador1.nombre);
			avanceJugador [index] = avanceJugador1;
			Principal.Log ("Sobrescribir OK" + avanceJugador1.nombre);
		} else {
			nombreJugador.Add(avanceJugador1.nombre);
			avanceJugador.Add(avanceJugador1);
			Principal.Log("Sobrescribir agrego nuevo usuario" + avanceJugador1.nombre);
		}
	}

	public AvanceJugador avanceJugadorActualPorNombre(string nombre) {
		if (nombreJugador.Contains (nombre)) {
			int index = nombreJugador.IndexOf (Principal.user);
			AvanceJugador avaj = avanceJugador [index];
			//    Debug.Log("avance jugador existente" + Principal.user);
			return avaj;
		} else
			return null;
	} 

    public AvanceJugador avanceJugadorActual() {
        if (nombreJugador.Contains(Principal.user))
        {
            int index = nombreJugador.IndexOf(Principal.user);
            AvanceJugador avaj = avanceJugador[index];
        //    Debug.Log("avance jugador existente" + Principal.user);
			ultimoUsuario=Principal.user;
            return avaj;
        }
        else {
            AvanceJugador avaj = new AvanceJugador();
            avaj.nombre = Principal.user;
            nombreJugador.Add(Principal.user);
            avanceJugador.Add(avaj);
			Principal.Log("avance jugador nuevo" + Principal.user);
			ultimoUsuario=Principal.user;
			return avaj;
        }
    } 

    public void actualizar(string jugador,string actividad, int nivel, int puntaje,int puntajeMaximo)
    {
		Principal.Log("puntaje " + jugador + " " + actividad + " " + nivel + " = " + puntaje);
        if (nombreJugador.Contains(jugador))
        {
			Principal.Log("jugador existente");
            int index = nombreJugador.IndexOf(jugador);
            AvanceJugador avaj = avanceJugador[index];
            avaj.actualizar(actividad,nivel, puntaje, puntajeMaximo);
        }
        else
        {
			Principal.Log("jugador nuevo");
            AvanceJugador avaj= new AvanceJugador();
            avaj.nombre = Principal.user;
            avaj.actualizar(actividad,nivel, puntaje, puntajeMaximo);
            nombreJugador.Add(jugador);
            avanceJugador.Add(avaj);
        }
    }

    public int puntaje(string jugador, string actividad, int nivel) {
        if (nombreJugador.Contains(jugador))
        {
            int index = nombreJugador.IndexOf(jugador);
            AvanceJugador avaj = avanceJugador[index];
            int r = avaj.puntaje(actividad, nivel);
            
//            Debug.Log("puntaje " + jugador + " " + actividad + " " + nivel + " = " + r);
            return r;
        }
        else {
            return 0;
        }
    }


}
