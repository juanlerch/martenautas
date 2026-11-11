using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabsMostrarOcultarPorTiempo : MonoBehaviour
{
	 double initialTime=0.0;
	public double deltaTime =0.0;
	public bool done=false;
    public GameObject[] mostrar;
    public GameObject[] ocultar;
      
    
	void Start () {
		initialTime = deltaTime ; 
	}
    void OnDisable()
    {
        done = false;
    }
	void Update () {
		if (!done) {
			if (deltaTime  <= 0.0) {
				mostrarOcultar ();
				done = true;
                deltaTime= initialTime;
            } else {
			
				deltaTime -= Time.deltaTime; 
			}

		}

	}


	public void mostrarOcultar()
    {
        foreach (GameObject m in mostrar)
        {
            if (m!=null) m.SetActive(true);
        };
        foreach (GameObject o in ocultar)
        {
            if (o != null) o.SetActive(false);
        }

    }




}
