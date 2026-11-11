using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Avance  {

    string ultimoUsuario;

    public List<string>        nombreJugador = new List<string>();
    public List<AvanceJugador> avanceJugador= new List<AvanceJugador>();


    public void init()
    {

    }

    public AvanceJugador avanceJugadorActual() {
        if (nombreJugador.Contains(Principal.user))
        {
            int index = nombreJugador.IndexOf(Principal.user);
            AvanceJugador avaj = avanceJugador[index];
        //    Debug.Log("avance jugador existente" + Principal.user);
            return avaj;
        }
        else {
            AvanceJugador avaj = new AvanceJugador();
            avaj.nombre = Principal.user;
            nombreJugador.Add(Principal.user);
            avanceJugador.Add(avaj);
            Debug.Log("avance jugador nuevo" + Principal.user);
            return avaj;
        }
    } 

    public void actualizar(string jugador,string actividad, int nivel, int puntaje,int puntajeMaximo)
    {
        Debug.Log("puntaje " + jugador + " " + actividad + " " + nivel + " = " + puntaje);
        if (nombreJugador.Contains(jugador))
        {
            Debug.Log("jugador existente");
            int index = nombreJugador.IndexOf(jugador);
            AvanceJugador avaj = avanceJugador[index];
            avaj.actualizar(actividad,nivel, puntaje, puntajeMaximo);
        }
        else
        {
            Debug.Log("jugador nuevo");
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
            
            Debug.Log("puntaje " + jugador + " " + actividad + " " + nivel + " = " + r);
            return r;
        }
        else {
            return 0;
        }
    }


}
