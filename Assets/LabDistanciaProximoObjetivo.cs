using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabDistanciaProximoObjetivo : MonoBehaviour {

	public GameObject personaje;
	public GameObject[] objetivosIndividual;
	public GameObject   objetivosHijosDe;
	public int escala = 10000;

	public string sufijo = "M";

	public string excluirNombres="Boid";

	List<GameObject> objetivos = new List<GameObject>();

	void Start(){
		if (objetivosHijosDe != null)
			foreach (Transform child in objetivosHijosDe.transform)
				if(child !=null )
				if (excluirNombres==null || excluirNombres.Length==0) 
						objetivos.Add (child.gameObject );
					else 
						if	(!child.name.StartsWith(excluirNombres)) 
							objetivos.Add (child.gameObject );

		if (objetivosIndividual != null) 
				foreach(GameObject go in objetivosIndividual ) 
					if(go!=null)objetivos.Add (go);
	}

	public float FindClosest()
	{
		
		//objetivos = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = personaje.transform.position;
		foreach (GameObject go in objetivos.ToArray())
		{
			if (go != null && go.activeSelf) {
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
					closest = go;
					distance = curDistance;
				}
			} else
				if (go != null &&!go.activeSelf)  objetivos.Remove (go);
//			if(closest!=null) Debug.Log ("Objetivo" + closest.name);
		}
		if (closest == null)
			return 0.0f;
		else
			return distance;
		//return closest;
	}

	int lastdistance;
	// Update is called once per frame
	void Update () {
		Text text = GetComponent<Text> ();
		float near = FindClosest ();
		int inear = (int) near / escala;
		inear = inear * 10;
		text.text = "" + inear + " " + sufijo;
	}
}
