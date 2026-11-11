using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AvanceJugador  {
    public string nombre; 
    public string clave;
	public string token;

	public int avatarID=-1;
	public int idActualizacion=0;

    public bool mantenerLogin = true;
    
    public string nivel;//el rango del jugador
    public int    puntajeTotal;


    public AvancePreferencias preferencias = new AvancePreferencias();

    public List<AvanceLogro> logros;
	//public List<string> insignias= new List<string>();

    public List<string> mostrado = new List<string>();

	public List<string> leido = new List<string>();

    public List<string> nombreActividad = new List<string>();

    public List<AvanceActividad> avanceActividad = new List<AvanceActividad>();

	public List<string> desbloqueo = new List<string>(); 

	public bool esDesbloqueado(string name){
		if ((desbloqueo!=null) && desbloqueo.Contains(name)) 
			return true;
		else 
			return false;
	}

	public void desbloquear(string name){
		if (desbloqueo == null)
			desbloqueo = new List<string> ();

		if (!desbloqueo.Contains (name)) {
			desbloqueo.Add (name);
		}
	
	}


	public void agregarLeido(string sleido){
		if (leido == null ) {
			leido = new List<string>(); 
		}
		if(sleido!=null && sleido.Length>0 && !leido.Contains(sleido)) leido.Add(sleido);
	
	}

    public AvanceLogro obtenerLogro(string logro) {
        if (logros == null) logros = new List<AvanceLogro>();
        foreach (AvanceLogro l in  logros){
            if (l.nombre == logro) return l;
        }
        return null;
    }

	public void  borrarLogro(string logro) {
		if (logros == null) logros = new List<AvanceLogro>();
		foreach (AvanceLogro l in  logros){
			if (l.nombre == logro) {
				logros.Remove(l);
				return;
			}
		}
		return ;
	}

    public void agregarLogro(string logro,int nivel,int maxnivel)
    {
        if (logros == null) logros = new List<AvanceLogro>();
        AvanceLogro l = obtenerLogro(logro);
        if (l == null)
        {
            l = new AvanceLogro();
            logros.Add(l);

        }
                l.nombre = logro;
        l.nivel = nivel;
        l.maxNivel = maxnivel;


    }

	public AvanceEstadistica estadistica (string actividad,int nivel){
		if (nombreActividad.Contains (actividad)) {
			int index = nombreActividad.IndexOf (actividad);
			AvanceActividad ava = avanceActividad [index];
			AvanceEstadistica ae= ava.estadistica (nivel);
			return ae;
		} else {
			AvanceActividad ava = new AvanceActividad();
			ava.nombre = actividad;
			nombreActividad.Add(actividad);
			avanceActividad.Add(ava);
			AvanceEstadistica ae= ava.estadistica (nivel);
			return ae;
		}
	}
    


    public void actualizar(string actividad, int nivel, int puntaje,int puntaleMaximo) {
        if (nombreActividad.Contains(actividad)) {
            int index = nombreActividad.IndexOf(actividad);
            AvanceActividad ava = avanceActividad[index];
            ava.nombre = actividad;
            int puntajeActividad = ava.puntaje(nivel);
            if (puntajeActividad < puntaje) {
                puntajeTotal += (puntaje- puntajeActividad);
            }
            ava.actualizar(nivel, puntaje, puntaleMaximo);
        }
        else
        {
            AvanceActividad ava = new AvanceActividad();
            ava.nombre = actividad;
            ava.actualizar(nivel, puntaje, puntaleMaximo);
            nombreActividad.Add(actividad);
            avanceActividad.Add(ava);
            puntajeTotal += puntaje; 
        }
    }

    //0 No iniciada. 1//iniciada //2 completa (100%)
    public int estadoActividad(string actividad, int nivel) {

        if (nombreActividad.Contains(actividad))
        {
            Debug.Log("estado actividad encontrada" + actividad);

            int index = nombreActividad.IndexOf(actividad);
            AvanceActividad ava = avanceActividad[index];
            int r = ava.puntaje(nivel);
            int m = ava.puntajeMaximo(nivel);

            if (r>0 && r >= m ) return 2;      //completa 
            else if (r > 0) return 1; //iniciada
            else return 0;           //no empezada
        }
        else {
            foreach(string s in nombreActividad) Debug.Log(s);
            Debug.Log("estado actividad NO encontrada" + actividad);

            return 0;
        }


    }

    public int puntaje(string actividad, int nivel)
    {
        if (nombreActividad.Contains(actividad))
        {
            int index = nombreActividad.IndexOf(actividad);
            AvanceActividad ava = avanceActividad[index];
            int r = ava.puntaje(nivel);
            return r;
        }
        else {
            return 0;
        }
    }

    public int puntajeMaximo(string actividad, int nivel)
    {
        if (nombreActividad.Contains(actividad))
        {
            int index = nombreActividad.IndexOf(actividad);
            AvanceActividad ava = avanceActividad[index];
            int r = ava.puntajeMaximo(nivel);
            return r;
        }
        else {
            return 0;
        }
    }

}
