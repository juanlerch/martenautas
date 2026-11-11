using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Vuforia;

public class LabsVuforia : MonoBehaviour {
    static bool init = false;

    public GameObject cargando;
    public GameObject continuar;
	public GameObject errorPanel;

	public string actividad="Explora Sistema Solar";
	public int nivel=1;
	private int puntajeObtenido=1;
	private int puntajeMaximo=500;

    IEnumerator cargar()/*   StartCoroutine(esperarYJugar());*/
    {
        continuar.SetActive(false);
        cargando.SetActive(true);
        yield return new WaitForSeconds(1);

		if (actividad == null)
			actividad = "Explora Sistema Solar";
		if (nivel == 0) nivel = 1; 
		if (puntajeMaximo == 0)  puntajeMaximo = 10;
		if (puntajeObtenido ==0) puntajeObtenido=1;


        if (!init)
        {
			try{
			Debug.Log("iniciando vuforia");
            VuforiaRuntime.Instance.InitVuforia();
			Debug.Log("iniciando vuforia OK"); 
            VuforiaBehaviour.Instance.enabled = true;
            init = true;

            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
			}
			catch{
				Debug.Log("iniciando vuforia ERR user:"+ Principal.user +" act: "+ actividad+" niv: "+ nivel +" punt: "+ puntajeObtenido+" max: "+ puntajeMaximo);
				if (errorPanel != null) {
					errorPanel.SetActive (true);
					Debug.Log ("Error panel activo"); 
				}
				Principal.avance.actualizar(Principal.user, actividad, nivel, 1,puntajeMaximo);
			}
        }
        else {
            cargando.SetActive(false);
            continuar.SetActive(true);
        }
		Debug.Log("iniciando vuforia FIN");
		if (!init) {
			if(errorPanel!=null) errorPanel.SetActive(true);
			Principal.avance.actualizar(Principal.user, actividad, nivel, puntajeObtenido,puntajeMaximo);
		}
	}




    // Use this for initialization
    void Start () {

        StartCoroutine(cargar());

    }


    void OnDestroy()
    {
        if (VuforiaBehaviour.Instance != null) { 
            VuforiaBehaviour.Instance.enabled = false;
            //VuforiaRuntime.Instance.Deinit();
        }
    }

    // Update is called once per frame
    void Update () {


    }
}
