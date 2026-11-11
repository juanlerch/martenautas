using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Vuforia;

public class LabsVuforia : MonoBehaviour {
    static bool init = false;

    public GameObject cargando;
    public GameObject continuar;

    IEnumerator cargar()/*   StartCoroutine(esperarYJugar());*/
    {
        continuar.SetActive(false);
        cargando.SetActive(true);
        yield return new WaitForSeconds(1);

        if (!init)
        {
            VuforiaRuntime.Instance.InitVuforia();

            VuforiaBehaviour.Instance.enabled = true;
            init = true;

            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);

        }
        else {
            cargando.SetActive(false);
            continuar.SetActive(true);
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
