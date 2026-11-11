using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AvanceEstadistica {

	public int ejecuciones=0;

	public AvanceEstadisticaData primeraEjecucion;
	public AvanceEstadisticaData ultimaEjecucion;

	private AvanceEstadisticaData ejecucionActual;

	public void Inicializar (){
		ejecucionActual = new AvanceEstadisticaData ();
		ejecucionActual.fechaInicio = NewDateTime.CreateNewDateTime(System.DateTime.Now);
	}

	public void actualizarOK(){
		if (ejecucionActual == null) Inicializar ();
		ejecucionActual.aciertos++;
	}

	public void actualizarERR(string desc){
		if (ejecucionActual == null) Inicializar ();

		ejecucionActual.fallas++;
		if (ejecucionActual.primerError == null)
			ejecucionActual.primerError = desc;
	}
	 
	public void Finalizar (){
		if (ejecucionActual == null) Inicializar ();
		ejecucionActual.fechaFin = NewDateTime.CreateNewDateTime(System.DateTime.Now);
		if (primeraEjecucion == null || ejecuciones <= 0)
			primeraEjecucion = ejecucionActual;
		else
			ultimaEjecucion = ejecucionActual;
		ejecucionActual=null;
		ejecuciones++;
	}

}
