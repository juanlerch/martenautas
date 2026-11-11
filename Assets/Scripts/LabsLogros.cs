using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

[System.Serializable]
public class PostSubirAvance{
	public string token;
	public AvanceJugador avances;
}

public class LabsLogros : MonoBehaviour {
	public GameObject panelPlayer;
	string urlBase = Principal.urlBase;
	
	public static string volverA;

	public void volver(){
		Principal p = new Principal ();
		if (volverA != null) {
			string t=volverA;
			p.cargarEscena (t);
			volverA = null;
		} else {
			p.volver ();
		}
	}

	// Use this for initialization
	void Start () {
		if(panelPlayer!=null)
			panelPlayer.SetActive (false);


		subirLogrosAInternet ();
	}

	public void subirLogrosAInternet(){
		StartCoroutine(Upload());
	}

	IEnumerator Upload() {

		if(Principal.avance.internet && Principal.avance.avanceJugadorActual().token!=null){
					AvanceJugador avance = Principal.avance.avanceJugadorActual();
					PostSubirAvance post = new PostSubirAvance ();
					post.token = avance.token;
					post.avances = avance;
			 
					string jsonAvance = JsonUtility.ToJson (post, true);


					byte[] myData = System.Text.Encoding.UTF8.GetBytes(jsonAvance );
					string url = 	urlBase + "/subir-avance";
					UnityWebRequest www = UnityWebRequest.Put(url, myData);
					//UnityWebRequest www = UnityWebRequest.Post(url, jsonAvance);
					www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
					www.useHttpContinue = false;
					yield return www.SendWebRequest();

					if(www.isNetworkError || www.isHttpError) {
				         Debug.Log("Error subiendo avance!" + jsonAvance);
					}
					else { 
						Debug.Log("Upload complete!");
						if (www.downloadHandler != null) {
							string jsonResult = www.downloadHandler.text;
							//Debug.Log ("Upload:" + jsonResult); 
							AvanceResponse aresp = JsonUtility.FromJson<AvanceResponse> (jsonResult);

							Principal.avance.avanceJugadorActual ().idActualizacion = aresp.idActualizacion;
							Principal.guardarAvanceStatic ();
						}
					}
			}
	}

}
