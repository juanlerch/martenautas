using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LabsLogros : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(Upload ());
	}

	IEnumerator Upload(){
		string url = "http://localhost/martenautas/index.php/puntajes";

		AvanceJugador avance = Principal.avance.avanceJugadorActual();
		string jsonData = JsonUtility.ToJson (avance,true);

		UnityWebRequest www = UnityWebRequest.Post (url, jsonData);
		byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonData);
		www.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
		www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
		www.SetRequestHeader("Content-Type", "application/json");

		yield return www.Send();
		if (www.isNetworkError || www.isHttpError) {
				Debug.Log (www.error);
			} else {
				if (www.isDone) {
					string jsonResult = www.downloadHandler.text;
					Debug.Log (jsonResult);

				}
			}
		}

	
	// Update is called once per frame
	void Update () {
		
	}
}
